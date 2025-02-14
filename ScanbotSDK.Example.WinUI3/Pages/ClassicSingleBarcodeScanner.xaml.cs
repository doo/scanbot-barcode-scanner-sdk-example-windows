using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.UI.Xaml.Controls;
using ScanbotSDK.Barcode;
using Microsoft.UI.Xaml.Navigation;
using Windows.Devices.Enumeration;
using System.Collections.ObjectModel;

namespace Barcode.SDK.Example.Pages
{
    public sealed partial class ClassicSingleBarcodeScanner : Page
    {
        private ObservableCollection<DeviceInformation> cameras = new ObservableCollection<DeviceInformation>();
        private readonly BarcodeScannerConfiguration ClassicSingleConfiguration = new BarcodeScannerConfiguration
        {
            // The below limits the barcode types that will be scanned.
            // Comment out this line to get all supported types or specify the types you want to try explicitly.
            //AcceptedBarcodeFormats =
            //[
            //    BarcodeFormat.QrCode,
            //    BarcodeFormat.MicroQrCode,
            //    BarcodeFormat.Aztec
            //]
        };
        BarcodeRecognizer recognizer = new BarcodeRecognizer(new BarcodeScannerConfiguration());
        public ClassicSingleBarcodeScanner()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var devices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            foreach (var device in devices)
            {
                cameras.Add(device);
            }

            if (devices.Count > 0)
            {
                ClassicSingleConfiguration.Camera.DeviceId = devices[0].Id;
            }
            
            await BarcodeScanner.Initialize(ClassicSingleConfiguration);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            BarcodeScanner.Dispose();
        }

        private async void OnError(Exception error)
        {
            BarcodeScanner.IsPaused = true;
            var contentDialog = new ContentDialog
            {
                Title = "An error occured",
                Content = error.Message,
                XamlRoot = XamlRoot,
                CloseButtonText = "Dismiss"
            };

            await contentDialog.ShowAsync();
            BarcodeScanner.IsPaused = false;
        }

        private async void OnRecognized(BarcodeItem[] barcodes)
        {
            if (barcodes.Length == 0)
            {
                return;
            }

            BarcodeScanner.IsPaused = true;

            var joinedBarcodes = string.Join(", ", barcodes.Select(b => b.Text));
            var toastTitle = barcodes.Length > 1 ?
                "Detected multiple barcodes" :
                $"Detected {barcodes[0].Format} barcode";

            var contentDialog = new ContentDialog
            {
                Title = toastTitle,
                Content = joinedBarcodes,
                XamlRoot = XamlRoot,
                CloseButtonText = "Close"
            };

            await contentDialog.ShowAsync();

            BarcodeScanner.IsPaused = false;
        }

        private async void CameraSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            var camera = (DeviceInformation)e.AddedItems[0];
            ClassicSingleConfiguration.Camera.DeviceId = camera.Id;
            await BarcodeScanner.Initialize(ClassicSingleConfiguration);
        }
    }
}

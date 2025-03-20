using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using ScanbotSDK.Barcode;
using ScanbotSDK.Barcode.UI;
using ScanbotSDK.Common;
using System;
using System.Collections.ObjectModel;
using Windows.Devices.Enumeration;

namespace Barcode.SDK.Example.Pages
{
    public sealed partial class ClassicMultipleBarcodeScanner : Page
    {
        private ObservableCollection<DeviceInformation> cameras = new ObservableCollection<DeviceInformation>();
        private readonly BarcodeScannerConfiguration ClassicMultipleConfiguration = new BarcodeScannerConfiguration
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

        public ClassicMultipleBarcodeScanner()
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
                ClassicMultipleConfiguration.Camera.DeviceId = devices[0].Id;
            }

            await BarcodeScanner.Initialize(ClassicMultipleConfiguration);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            BarcodeScanner.Dispose();
        }

        private async void OnError(Exception error)
        {
            var contentDialog = new ContentDialog
            {
                Title = "Oops! Something went wrong",
                Content = error.Message
            };

            await contentDialog.ShowAsync();
        }

        private void OnPolygonTapped(object sender, BarcodeFigure e)
        {
            var selectionOverlay = sender as SelectionOverlayControl;
            
            if (selectionOverlay == null)
            {
                return;
            }    

            if (!selectionOverlay.HighlightedBarcodes.Contains(e.Barcode))
            {
                e.PolygonStroke = selectionOverlay.HighlightedPolygonColor.ToWinUIBrush();
                e.PolygonFill = selectionOverlay.HighlightedPolygonColor.ToWinUIBrush(0.3);
                e.TextBackground = selectionOverlay.HighlightedTextBackgroundColor.ToWinUIBrush(0.7);
                e.TextColor = selectionOverlay.HighlightedTextColor.ToWinUIBrush();
                selectionOverlay.HighlightedBarcodes.Add(e.Barcode);
                //Scanbot.Service.SoundManager.Instance.Beep();
            }
            else
            {
                // removing the barcode will cause it to reset to the default colors 
                // specified via the `PolygonColor`, `TextBackgroundColor` and `HighlightedTextColor` properties,
                // thus no need to specify them manually.
                selectionOverlay.HighlightedBarcodes.Remove(e.Barcode);
            }
        }

        private Tuple<string, TextAlignment> OnFormatText(BarcodeItem arg)
        {
            const int maxLength = 25;
            var barcodeText = arg.Text.Length > maxLength ? $"{arg.Text.Substring(0, maxLength)}..." : arg.Text;
            var previewText = $"{arg.Format}: {barcodeText}";

            return Tuple.Create(previewText, TextAlignment.Left);
        }

        private async void CameraSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            var camera = (DeviceInformation)e.AddedItems[0];
            ClassicMultipleConfiguration.Camera.DeviceId = camera.Id;
            await BarcodeScanner.Initialize(ClassicMultipleConfiguration);
        }
    }
}

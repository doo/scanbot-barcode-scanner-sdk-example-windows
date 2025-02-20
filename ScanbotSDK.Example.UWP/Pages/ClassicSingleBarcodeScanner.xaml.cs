using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Barcode.SDK.Example.Utils;
using System.Linq;
using ScanbotSDK.Barcode;
using System;

namespace Barcode.SDK.Example.Pages
{
    public sealed partial class ClassicSingleBarcodeScanner : Page
    {
        private SystemNavigationManager BackButton;

        private readonly BarcodeScannerConfiguration ClassicSingleConfiguration = new BarcodeScannerConfiguration
        {
            // Uncomment to set predefined types
            // AcceptedBarcodeFormats = BarcodeFormats.Twod
            // Uncomment to set explicit types
            // AcceptedBarcodeFormats = new BarcodeFormat[] { BarcodeFormat.QrCode, BarcodeFormat.MicroQrCode, BarcodeFormat.Aztec }
        };

        public ClassicSingleBarcodeScanner()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await BarcodeScanner.Initialize(ClassicSingleConfiguration);

            BackButton = SystemNavigationManager.GetForCurrentView();
            BackButton.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            BackButton.BackRequested += OnBackPress;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            BarcodeScanner.Dispose();
            BackButton.BackRequested -= OnBackPress;
        }

        private async void OnError(Exception error)
        {
            await Toast.Show(error.Message, "Oops! Something went wrong");
        }

        private void OnBackPress(object sender, BackRequestedEventArgs e)
        {
            Frame.GoBack();
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

            await Toast.Show(joinedBarcodes, toastTitle);

            BarcodeScanner.IsPaused = false;
        }
    }
}

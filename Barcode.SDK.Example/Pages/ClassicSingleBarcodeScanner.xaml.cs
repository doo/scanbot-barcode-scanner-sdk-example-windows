using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Scanbot.Model;
using Barcode.SDK.Example.Utils;
using System.Linq;

namespace Barcode.SDK.Example.Pages
{
    public sealed partial class ClassicSingleBarcodeScanner : Page
    {
        private SystemNavigationManager BackButton;

        private readonly BarcodeScannerConfiguration ClassicSingleConfiguration = new BarcodeScannerConfiguration
        {
            // The below limits the barcode types that will be scanned.
            // Comment out this line to get all supported types or specify the types you want to try explicitly.
            AcceptedTypes = new List<BarcodeType> 
            { 
                BarcodeType.QRCode,
                BarcodeType.MicroQRCode,
                BarcodeType.Aztec
            }
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

        private async void OnError(ScanbotSdkException error)
        {
            await Toast.Show(error.Message, "Oops! Something went wrong");
        }

        private void OnBackPress(object sender, BackRequestedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void OnRecognized(BarcodeResult barcode)
        {
            if (barcode.IsEmpty)
            {
                return;
            }

            var joinedBarcodes = string.Join(", ", barcode.Barcodes.Select(b => b.Text));
            var toastTitle = barcode.Barcodes.Count > 1 ? 
                "Detected multiple barcodes" : 
                $"Detected {barcode.Barcodes[0].Type} barcode";

            await Toast.Show(joinedBarcodes, toastTitle);
        }
    }
}

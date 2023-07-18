using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Scanbot.Controls;
using Scanbot.Model;
using Scanbot.Utils;
using Barcode.SDK.Example.Utils;

namespace Barcode.SDK.Example.Pages
{
    public sealed partial class ClassicMultipleBarcodeScanner : Page
    {
        private SystemNavigationManager BackButton;

        private readonly BarcodeScannerConfiguration ClassicMultipleConfiguration = new BarcodeScannerConfiguration
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

        public ClassicMultipleBarcodeScanner()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await BarcodeScanner.Initialize(ClassicMultipleConfiguration);

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

        private void OnPolygonTapped(object sender, BarcodeFigure e)
        {
            var selectionOverlay = sender as SelectionOverlayControl;
            
            if (selectionOverlay == null)
            {
                return;
            }    

            if (!selectionOverlay.HighlightedBarcodes.Contains(e.Barcode))
            {
                e.PolygonStroke = selectionOverlay.HighlightedPolygonColor.Brush();
                e.PolygonFill = selectionOverlay.HighlightedPolygonColor.Brush(0.3);
                e.TextBackground = selectionOverlay.HighlightedTextBackgroundColor.Brush(0.7);
                e.TextColor = selectionOverlay.HighlightedTextColor.Brush();
                selectionOverlay.HighlightedBarcodes.Add(e.Barcode);
                Scanbot.Service.SoundManager.Instance.Beep();
            }
            else
            {
                // removing the barcode will cause it to reset to the default colors 
                // specified via the `PolygonColor`, `TextBackgroundColor` and `HighlightedTextColor` properties,
                // thus no need to specify them manually.
                selectionOverlay.HighlightedBarcodes.Remove(e.Barcode);
            }
        }

        private Tuple<string, TextAlignment> OnFormatText(Scanbot.Model.Barcode arg)
        {
            const int maxLength = 25;
            var barcodeText = arg.Text.Length > maxLength ? $"{arg.Text.Substring(0, maxLength)}..." : arg.Text;
            var previewText = $"{arg.Type}: {barcodeText}";

            return Tuple.Create(previewText, TextAlignment.Left);
        }
    }
}

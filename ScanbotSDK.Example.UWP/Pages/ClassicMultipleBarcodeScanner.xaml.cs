using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Barcode.SDK.Example.Utils;
using ScanbotSDK.Barcode;
using ScanbotSDK.Barcode.UI;
using ScanbotSDK.Common;

namespace Barcode.SDK.Example.Pages
{
    public sealed partial class ClassicMultipleBarcodeScanner : Page
    {
        private SystemNavigationManager BackButton;

        private readonly BarcodeScannerConfiguration ClassicMultipleConfiguration = new BarcodeScannerConfiguration
        {
            // Uncomment to set predefined types
            // AcceptedBarcodeFormats = BarcodeFormats.Twod
            // Uncomment to set explicit types
            // AcceptedBarcodeFormats = [ BarcodeFormat.QrCode, BarcodeFormat.MicroQrCode, BarcodeFormat.Aztec ]
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

        private async void OnError(Exception error)
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
                e.PolygonStroke = selectionOverlay.HighlightedPolygonColor.ToWinUIBrush();
                e.PolygonFill = selectionOverlay.HighlightedPolygonColor.ToWinUIBrush(0.3);
                e.TextBackground = selectionOverlay.HighlightedTextBackgroundColor.ToWinUIBrush(0.7);
                e.TextColor = selectionOverlay.HighlightedTextColor.ToWinUIBrush();
                selectionOverlay.HighlightedBarcodes.Add(e.Barcode);
                SoundManager.PlayBeep();
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
    }
}

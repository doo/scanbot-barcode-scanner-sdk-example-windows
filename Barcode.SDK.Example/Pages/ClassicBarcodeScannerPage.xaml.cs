using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Scanbot.Model;
using Scanbot.Utils;
using Barcode.SDK.Example.Utils;

namespace Barcode.SDK.Example.Properties
{
    public sealed partial class ClassicBarcodeScannerPage : Page
    {
        private SystemNavigationManager BackButton;

        private readonly BarcodeScannerConfiguration ClassicComponentConfiguration = new BarcodeScannerConfiguration
        {
            // The below limits the barcode types that will be scanned.
            // Comment out this line to get all supported types or specify the types you want to try explicitly.
            AcceptedTypes = new List<BarcodeType> { BarcodeType.QRCode, BarcodeType.Aztec }
        };

        public ClassicBarcodeScannerPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await BarcodeScanner.Initialize(ClassicComponentConfiguration);

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

        private void OnError(ScanbotSdkException error)
        {
            Toast.Show(error.Message, "Oops! Something went wrong");
        }

        private void OnBackPress(object sender, BackRequestedEventArgs e)
        {
            Frame.GoBack();
        }

        private void SelectionOverlayPolygonTapped(object sender, BarcodeFigure e)
        {
            if (!SelectionOverlay.HighlightedBarcodes.Contains(e.Barcode))
            {
                e.PolygonStroke = SelectionOverlay.HighlightedPolygonColor.Brush();
                e.PolygonFill = SelectionOverlay.HighlightedPolygonColor.Brush(0.3);
                e.TextBackground = SelectionOverlay.HighlightedTextBackgroundColor.Brush(0.7);
                e.TextColor = SelectionOverlay.HighlightedTextColor.Brush();
                SelectionOverlay.HighlightedBarcodes.Add(e.Barcode);
                Scanbot.Service.SoundManager.Instance.Beep();
            }
            else
            {
                // removing the barcode will cause it to reset to the default colors 
                // specified via the `PolygonColor`, `TextBackgroundColor` and `HighlightedTextColor` properties,
                // thus no need to specify them manually.
                SelectionOverlay.HighlightedBarcodes.Remove(e.Barcode);
            }
        }

        private Tuple<string, TextAlignment> SelectionOverlayFormatText(Scanbot.Model.Barcode arg)
        {
            const int maxLength = 25;
            var barcodeText = arg.Text.Length > maxLength ? $"{arg.Text.Substring(0, maxLength)}..." : arg.Text;
            var previewText = $"{arg.Type}: {barcodeText}";

            return Tuple.Create(previewText, TextAlignment.Left);
        }
    }
}

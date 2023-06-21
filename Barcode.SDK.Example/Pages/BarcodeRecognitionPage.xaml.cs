using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Scanbot.Controls;
using Scanbot.Model;
using Barcode.SDK.Example.Utils;

namespace Barcode.SDK.Example.Properties
{
    public sealed partial class BarcodeRecognitionPage : Page
    {
        private SystemNavigationManager BackButton;

        private readonly BarcodeScannerConfiguration Configuration;

        public BarcodeRecognitionPage()
        {
            InitializeComponent();
            Configuration = new BarcodeScannerConfiguration();
            Configuration.AcceptedTypes = new List<BarcodeType> { BarcodeType.QRCode, BarcodeType.Aztec };
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await BarcodeScannerComponent.Initialize(Configuration);

            BarcodeScannerComponent.Recognized += OnBarcodeResult;
            BarcodeScannerComponent.Error += OnError;

            BackButton = SystemNavigationManager.GetForCurrentView();
            BackButton.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            BackButton.BackRequested += OnBackPress;

            Overlay.Continue.Click += OnContinue;
            Overlay.Close.Click += OnClose;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            BarcodeScannerComponent.Dispose();

            BarcodeScannerComponent.Recognized -= OnBarcodeResult;
            BarcodeScannerComponent.Error -= OnError;
            BackButton.BackRequested -= OnBackPress;

            Overlay.Continue.Click -= OnContinue;
            Overlay.Close.Click -= OnClose;
        }

        private void OnBarcodeResult(BarcodeResult result)
        {
            if (result.IsEmpty || BarcodeScannerComponent.IsPaused)
            {
                return;
            }

            BarcodeScannerComponent.IsPaused = true;
            Finder.Visibility = Visibility.Collapsed;
            Overlay.Show(result);
        }

        private void OnError(ScanbotSdkException error)
        {
            Toast.Show(error.Message, "Oops! Something went wrong");
        }

        private void OnBackPress(object sender, BackRequestedEventArgs e)
        {
            Frame.GoBack();
        }

        private void OnContinue(object sender, RoutedEventArgs e)
        {
            BarcodeScannerComponent.IsPaused = false;
            Finder.Visibility = Visibility.Visible;
            Overlay.Hide();
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}

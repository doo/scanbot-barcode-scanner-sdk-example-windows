using Barcode.SDK.Example.Utils;
using Scanbot.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Barcode.SDK.Example.Properties
{
    public sealed partial class BarcodeRecognitionPage : Page
    {
        SystemNavigationManager BackButton = SystemNavigationManager.GetForCurrentView();

        public BarcodeRecognitionPage()
        {
            InitializeComponent();

            BarcodeScannerComponent.Padding = new Thickness(30, 30, 30, 30);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var configuration = new BarcodeScannerConfiguration
            {
            };

            await BarcodeScannerComponent.Initialize(configuration);

            BarcodeScannerComponent.Callback += OnBarcodeResult;
            BarcodeScannerComponent.Error += OnError;

            BackButton.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            BackButton.BackRequested += OnBackPress;
        }

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            await BarcodeScannerComponent.Dispose();

            BarcodeScannerComponent.Callback -= OnBarcodeResult;
            BarcodeScannerComponent.Error -= OnError;

            BackButton.BackRequested -= OnBackPress;
        }

        private void OnBarcodeResult(BarcodeResult result)
        {
            if (result.IsEmpty)
                return;
            Toast.Show(result.Barcodes);
        }

        private void OnError(Error error)
        {
            Toast.Show(error.Message, "Oops! Something went wrong");
        }

        private void OnBackPress(object sender, BackRequestedEventArgs e)
        {
            Frame.GoBack();
        }

    }
}

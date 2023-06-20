using Barcode.SDK.Example.Utils;
using BarcodeSDK.Example.Controls;
using Scanbot.Controls;
using Scanbot.Model;
using Scanbot.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        Scanbot.Model.BarcodeScannerConfiguration Configuration;

        BarcodeFoundOverlay Overlay;

        public BarcodeRecognitionPage()
        {
            InitializeComponent();

            BarcodeScannerComponent.Padding = new Thickness(30, 30, 30, 30);

            Configuration = new BarcodeScannerConfiguration();
            //Configuration.Finder.Hint = "Custom finder hint text...";
            //Configuration.AcceptedTypes = new BarcodeType[] { BarcodeType.QRCode, BarcodeType.Aztec }.ToList();

            Overlay = new BarcodeFoundOverlay();
            Root.Children.Add(Overlay);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await BarcodeScannerComponent.Initialize(Configuration);

            BarcodeScannerComponent.Recognized += OnBarcodeResult;
            BarcodeScannerComponent.Error += OnError;

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
            if (result.IsEmpty)
                return;

            if (BarcodeScannerComponent.IsPaused)
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

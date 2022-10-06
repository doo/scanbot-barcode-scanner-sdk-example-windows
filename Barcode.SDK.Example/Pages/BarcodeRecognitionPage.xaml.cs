using Barcode.SDK.Example.Utils;
using BarcodeSDK.Example.Controls;
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

        BarcodeScannerConfiguration Configuration = new BarcodeScannerConfiguration
        {
        };

        BarcodeFoundOverlay Overlay;

        public BarcodeRecognitionPage()
        {
            InitializeComponent();

            BarcodeScannerComponent.Padding = new Thickness(30, 30, 30, 30);

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

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            await BarcodeScannerComponent.Dispose();

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

            ViewUtils.RunOnMain(() =>
            {
                BarcodeScannerComponent.IsPaused = true;
                HideFinder();
                Overlay.Show(result);
            });
            
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

        private void OnContinue(object sender, RoutedEventArgs e)
        {
            BarcodeScannerComponent.IsPaused = false;
            ShowFinder();
            Overlay.Hide();
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        void ShowFinder()
        {
            BarcodeScannerComponent.ViewFinder.Hole.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
            BarcodeScannerComponent.ViewFinder.Hole.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
            BarcodeScannerComponent.ViewFinder.Hint.Visibility = Visibility.Visible;
        }

        void HideFinder()
        {
            BarcodeScannerComponent.ViewFinder.Hole.Fill = new SolidColorBrush(Configuration.Finder.Background);
            BarcodeScannerComponent.ViewFinder.Hole.Stroke = null;
            BarcodeScannerComponent.ViewFinder.Hint.Visibility = Visibility.Collapsed;
        }
    }
}

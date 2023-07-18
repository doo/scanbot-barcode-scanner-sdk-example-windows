using System;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Scanbot;
using Scanbot.Model;
using Barcode.SDK.Example.Model;
using Barcode.SDK.Example.Pages;
using Barcode.SDK.Example.Utils;

namespace Barcode.SDK.Example
{
    public sealed partial class MainPage : Page
    {
        private readonly BarcodeScanner Scanner = new BarcodeScanner();

        private readonly BarcodeScannerConfiguration Configuration = new BarcodeScannerConfiguration
        {
            // This configuration is used for the ready-to-use UI as well as scanning from a file.
            // Add/remove properties as needed to change the way the scanner behaves.
            // See ClassicComponentBarcodeScannerPage to change its configuration.
        };

        public MainPage()
        {
            InitializeComponent();

            List.ItemsSource = Feature.ExampleFeatures;
            List.IsItemClickEnabled = true;

            Background = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230));

            LicenseLabel.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            LicenseLabel.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            LicenseLabel.TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
            LicenseLabelContainer.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 200, 25, 60));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;

            base.OnNavigatedTo(e);

            var errorMessage = "";

            if (string.IsNullOrEmpty(App.LICENSE_KEY))
            {
                errorMessage = "Missing trial license key! Please see the comments in App.xaml.cs";
            }
            else if (!LicenseManager.Details.IsValid)
            {
                errorMessage = LicenseManager.Details.Description;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                LicenseLabelContainer.Visibility = Windows.UI.Xaml.Visibility.Visible;
                LicenseLabel.Text = errorMessage;
                List.SelectionMode = ListViewSelectionMode.None;
                List.IsItemClickEnabled = false;
            }

            List.ItemClick += OnItemClickAsync;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            List.ItemClick -= OnItemClickAsync;
        }

        private async void OnItemClickAsync(object sender, ItemClickEventArgs e)
        {
            var item = (Feature)e.ClickedItem;

            if (item.Type == Feature.FeatureType.ReadyToUseUI)
            {
                var result = await Scanner.Start(Frame, Configuration);
                await Toast.Show(result.Barcodes);
            }
            else if (item.Type == Feature.FeatureType.ClassicComponentSingle)
            {
                Frame.Navigate(typeof(ClassicSingleBarcodeScanner));
            }
            else if (item.Type == Feature.FeatureType.ClassicComponentMultiple)
            {
                Frame.Navigate(typeof(ClassicMultipleBarcodeScanner));
            }
            else if (item.Type == Feature.FeatureType.ImportImage)
            {
                var bitmap = await FileUtils.PickImage();

                if (bitmap == null)
                {
                    return;
                }

                try
                {
                    var result = Scanner.Recognize(bitmap, Configuration);

                    if (result.Barcodes.Count == 0)
                    {
                        await Toast.Show("Didn't find any barcodes on the image you selected", "Oops");
                        return;
                    }
                    await Toast.Show(result.Barcodes);
                } 
                catch (Exception exception)
                {
                    await Toast.Show(exception.Message, "Oops");
                }
            }
        }

    }
}

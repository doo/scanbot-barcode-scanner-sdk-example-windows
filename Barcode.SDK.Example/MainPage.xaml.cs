
using Barcode.SDK.Example.Model;
using Barcode.SDK.Example.Properties;
using Barcode.SDK.Example.Utils;
using Scanbot;
using Scanbot.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Barcode.SDK.Example
{
    public sealed partial class MainPage : Page
    {
        BarcodeScanner Scanner = new BarcodeScanner();

        BarcodeScannerConfiguration Configuration = new BarcodeScannerConfiguration {};

        public MainPage()
        {
            InitializeComponent();


            List.ItemsSource = Feature.List;
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

            if (!LicenseManager.Details.IsValid)
            {
                LicenseLabelContainer.Visibility = Windows.UI.Xaml.Visibility.Visible;
                LicenseLabel.Text = LicenseManager.Details.Description;
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

            if (item == null) { return; }

            if (item.Id == 0)
            {
                var result = await Scanner.Start(Frame, Configuration);
                Toast.Show(result.Barcodes);
            }
            else if (item.Id == 1)
            {
                Frame.Navigate(typeof(BarcodeRecognitionPage));
            }
            else if (item.Id == 2)
            {
                var bitmap = await FileUtils.Pick();

                if (bitmap == null)
                {
                    return;
                }

                try
                {
                    var result = Scanner.Recognize(bitmap, Configuration);

                    if (result.Barcodes.Count == 0)
                    {
                        Toast.Show("Didn't find any barcodes on the image you selected", "Oops");
                        return;
                    }
                    Toast.Show(result.Barcodes);
                } catch(Exception exception)
                {
                    Toast.Show(exception.Message, "Oops");
                }
            }
        }

    }
}

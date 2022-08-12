
using Barcode.SDK.Example.Model;
using Barcode.SDK.Example.Utils;
using Scanbot;
using Scanbot.Model;
using System.Linq;
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

        BarcodeScannerConfiguration Configuration = new BarcodeScannerConfiguration
        {
            //Callback = (BarcodeResult result) =>
            //{
            //    if (result.Barcodes.Count == 0)
            //    {
            //        return;
            //    }

            //    Toast.Show(result.Barcodes);
            //},
            //Error = (Error error) =>
            //{
            //    Toast.Show(error.Message, "Oops! Something went terribly wrong");
            //},
        };

        public MainPage()
        {
            InitializeComponent();


            List.ItemsSource = Feature.List;

            Background = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private async void OnSelect(object sender, SelectionChangedEventArgs e)
        {
            // Get the instance of ListView
            Feature item = (sender as ListView).SelectedItem as Feature;

            if (item == null) { return; }

            if (item.Id == 0)
            {
                var result = await Scanner.Start(Frame, Configuration);
            }
            else if (item.Id == 1)
            {
                var bitmap = await FileUtils.Pick();

                if (bitmap == null)
                {
                    return;
                }

                var result = Scanner.Recognize(bitmap, Configuration);

                if (result.Barcodes.Count == 0)
                {
                    return;
                }
                Toast.Show(result.Barcodes);
            }
        }
    }
}

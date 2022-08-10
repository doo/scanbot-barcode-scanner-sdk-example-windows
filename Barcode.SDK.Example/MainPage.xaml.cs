﻿
using Barcode.SDK.Example.Model;
using Barcode.SDK.Example.Utils;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Barcode.SDK.Example
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();


            List.ItemsSource = Feature.List;

            Background = new SolidColorBrush(Colors.White);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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

            var configuration = new BarcodeScannerConfiguration
            {
                Callback = (BarcodeResult result) =>
                {
                    if (result.Barcodes.Count == 0)
                    {
                        return;
                    }

                    Toast.Show(result.Barcodes);
                },
                Error = (Error error) =>
                {
                    Toast.Show(error.Message, "Oops! Something went terribly wrong");
                },
                //RenderCroppedImage = true
            };

            if (item.Id == 0)
            {
                Scanner.Start(Frame, configuration);
            }
            else if (item.Id == 1)
            {
                var bitmap = await FileUtils.Pick();

                if (bitmap == null)
                {
                    return;
                }

                var result = Scanner.Recognize(bitmap, configuration);

                if (result.Barcodes.Count == 0)
                {
                    return;
                }
                Toast.Show(result.Barcodes[0].Text);
            }
        }
    }
}

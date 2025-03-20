using Barcode.SDK.Example.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.Storage;
using System.Threading.Tasks;
using ScanbotSDK.Barcode;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ScanbotSDK.Example.WinUI3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void StartSingleScanning(object sender, TappedRoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(ClassicSingleBarcodeScanner));
        }

        private void StartMultipleScanning(object sender, TappedRoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(ClassicMultipleBarcodeScanner));
        }

        private async void ImportImage(object sender, TappedRoutedEventArgs e)
        {
            var image = await PickImage();

            if (image == null)
            {
                return;
            }
            var recognizer = new BarcodeRecognizer(new BarcodeScannerConfiguration
            {
                Live = false,
                // Uncomment to set predefined types
                // AcceptedBarcodeFormats = BarcodeFormats.Twod
                // Uncomment to set explicit types
                // AcceptedBarcodeFormats = [ BarcodeFormat.QrCode, BarcodeFormat.MicroQrCode, BarcodeFormat.Aztec ]
            });
            var result = await recognizer.RecognizeAsync(image);

            if (result.Barcodes.Length == 0)
            {
                return;
            }

            var barcodeText = string.Join(", ", result.Barcodes.Select(b => $"{b.Format} {b.Text}"));
            var barcodeDialog = new ContentDialog()
            {
                XamlRoot = Content.XamlRoot,
                Title = "Barcodes detected",
                Content = barcodeText,
                CloseButtonText = "Ok"
            };

            await barcodeDialog.ShowAsync();
        }

        private async Task<SoftwareBitmap?> PickImage()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(this));

            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            string[] types = [".jpg", ".jpeg", ".png", ".bmp", ".gif"];
            
            foreach (var type in types)
            {
                picker.FileTypeFilter.Add(type);
            }

            var file = await picker.PickSingleFileAsync();

            if (file == null)
            {
                return null;
            }

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                // Get the SoftwareBitmap representation of the file
                return await decoder.GetSoftwareBitmapAsync();
            }
        }
    }
}

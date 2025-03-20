using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using ScanbotSDK.Barcode;

namespace ScanbotSDK.Example.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ItemSelected(object sender, RoutedEventArgs e)
        {
            if (selectionMenu.SelectedIndex == 0)
            {
                contentFrame.Navigate(new Uri("Pages/BarcodeScanner.xaml", UriKind.RelativeOrAbsolute));
            }
            else if (selectionMenu.SelectedIndex == 1)
            {
                selectionMenu.SelectedIndex = -1;
                var dialog = new OpenFileDialog
                {
                    Title = "Select an image file",
                    Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif"
                };

                dialog.ShowDialog(this);

                if (string.IsNullOrWhiteSpace(dialog.FileName))
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
                var image = new BitmapImage(new Uri(dialog.FileName));
                
                var result = await recognizer.RecognizeAsync(image);

                var barcodeText = string.Join(", ", result.Barcodes.Select(b => $"{b.Format} {b.Text}"));

                MessageBox.Show(this, barcodeText, "Barcodes detected", MessageBoxButton.OK);
            }
        }
    }
}
using ScanbotSDK.Barcode;
using ScanbotSDK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScanbotSDK.Example.WPF
{
    /// <summary>
    /// Interaction logic for BarcodeScanner.xaml
    /// </summary>
    public partial class BarcodeScanner : System.Windows.Controls.Page
    {
        private BarcodeRecognizer recognizer;

        public BarcodeScanner()
        {
            InitializeComponent();

            recognizer = new BarcodeRecognizer(new BarcodeScannerConfiguration());
            cameraPreview.ImageCaptured += CameraPreview_ImageCaptured;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var devices = MediaFoundation.GetVideoCaptureDevices();

            if (devices.Length > 0)
            {
                cameraPreview.CameraSymbolicLink = devices[0].SymbolicLink;
                cameraPreview.Start();
            }
        }

        private async void CameraPreview_ImageCaptured(IPlatformImage bitmap)
        {
            using var token = cameraPreview.PauseCapturing();
            var result = await recognizer.RecognizeAsync(bitmap);

            if (result.Barcodes.Length == 0)
            {
                return;
            }

            var barcodeText = string.Join(", ", result.Barcodes.Select(b => $"{b.Format} {b.Text}"));
            MessageBox.Show(barcodeText, "Barcodes detected");
        }
    }
}

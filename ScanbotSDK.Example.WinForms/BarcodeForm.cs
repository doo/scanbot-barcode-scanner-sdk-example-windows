using ScanbotSDK.Barcode;
using ScanbotSDK.Common;
using System.Diagnostics;

namespace ScanbotSDK.Example.WinForms
{
    public partial class Form1 : Form
    {
        private BarcodeRecognizer recognizer;

        public Form1()
        {
            InitializeComponent();
            recognizer = new BarcodeRecognizer(new BarcodeScannerConfiguration());
            listBox1.Items.Add("Single scanning");
            listBox1.Items.Add("Import an existing image");
            this.Load += Form1_Load;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            var devices = MediaFoundation.GetVideoCaptureDevices();

            if (devices.Length > 0)
            {
                cameraPreview.CameraSymbolicLink = devices[0].SymbolicLink;
            }
        }


        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                cameraPreview.Start();
            }
            else if (listBox1.SelectedIndex == 1)
            {
                listBox1.SelectedIndex = -1;
                cameraPreview.Stop();
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

                var image = System.Drawing.Image.FromFile(dialog.FileName);

                var barcodeResult = await recognizer.RecognizeAsync(image);

                var barcodeText = string.Join(", ", barcodeResult.Barcodes.Select(b => $"{b.Format} {b.Text}"));

                MessageBox.Show(this, barcodeText, "Barcodes detected", MessageBoxButtons.OK);
            }
        }

        private async void cameraPreview1_ImageCaptured(IPlatformImage image)
        {
            using var token = cameraPreview.PauseCapturing();
            var result = await recognizer.RecognizeAsync(image);

            if (result.Barcodes.Length == 0)
            {
                return;
            }

            var barcodeText = string.Join(", ", result.Barcodes.Select(b => $"{b.Format} {b.Text}"));
            MessageBox.Show(this, barcodeText, "Barcodes detected");
        }
    }
}

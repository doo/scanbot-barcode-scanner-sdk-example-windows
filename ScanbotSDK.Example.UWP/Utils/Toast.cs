using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using ScanbotSDK.Barcode;

namespace Barcode.SDK.Example.Utils
{
    public static class Toast
    {
        public static Task Show(BarcodeItem[] barcodes)
        {
            if (barcodes.Length == 1)
            {
                return Show(barcodes[0]);
            }

            var types = string.Join(", ", barcodes.Select(code => code.Format.ToString()));

            return Show(types, "Detected multiple barcodes!");
        }

        public static async Task Show(BarcodeItem barcode)
        {
            await Show(barcode.Text, "Detected " + barcode.Format);
        }

        private static Task pendingModal;

        public static async Task Show(string body, string title = "Barcode Detected!")
        {
            if (pendingModal != null)
            {
                return;
            }

            var message = new ContentDialog()
            {
                Title = title,
                Content = body,
                CloseButtonText = "Dismiss"
            };

            pendingModal = message.ShowAsync().AsTask();
            await pendingModal;
            pendingModal = null;
        }
    }
}

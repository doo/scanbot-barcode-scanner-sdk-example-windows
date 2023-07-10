
using Scanbot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Barcode.SDK.Example.Utils
{
    public static class Toast
    {
        public static Task Show(List<Scanbot.Model.Barcode> barcodes)
        {
            if (barcodes.Count == 1)
            {
                return Show(barcodes[0]);
            }

            var types = string.Join(", ", barcodes.Select(code => EnumExtensions.ToDescription(code.Type)));

            return Show(types, "Detected multiple barcodes!");
        }

        public static async Task Show(Scanbot.Model.Barcode barcode)
        {
            await Show(barcode.Text, "Detected " + barcode.Type);
        }

        public static async Task Show(string body, string title = "Barcode Detected!")
        {
            var message = new ContentDialog()
            {
                Title = title,
                Content = body,
                CloseButtonText = "Dismiss"
            };

            await message.ShowAsync();
        }
    }
}

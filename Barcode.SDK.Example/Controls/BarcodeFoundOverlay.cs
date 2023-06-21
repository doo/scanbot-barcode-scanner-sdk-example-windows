using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Scanbot.Model;
using Scanbot.Utils;

namespace BarcodeSDK.Example.Controls
{
    public class BarcodeFoundOverlay : StackPanel
    {
        TextBlock TextBlock { get; set; }
        
        public Button Continue { get; private set; }
        public Button Close { get; private set; }

        public BarcodeFoundOverlay()
        {
            Background = new SolidColorBrush(Color.FromArgb(150, 0, 0, 0));

            TextBlock = new TextBlock();
            TextBlock.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            TextBlock.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            TextBlock.FontSize = 20.0;
            TextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            TextBlock.FontWeight = Windows.UI.Text.FontWeights.Bold;
            TextBlock.Margin = new Windows.UI.Xaml.Thickness(0, 200, 0, 100);
            Children.Add(TextBlock);

            Continue = new Button();
            Continue.Content = "Continue";
            Continue.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            Continue.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Continue.FontSize = 18.0;
            Continue.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            Continue.Background = new SolidColorBrush(Windows.UI.Colors.Blue);
            Continue.Margin = new Windows.UI.Xaml.Thickness(0, 0, 0, 0);
            Continue.CornerRadius = new Windows.UI.Xaml.CornerRadius(5);
            Children.Add(Continue);

            Close = new Button();
            Close.Content = "Close";
            Close.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            Close.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Close.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            Close.Margin = new Windows.UI.Xaml.Thickness(0, 15, 0, 0);
            Close.FontSize = 16.0;
            Close.CornerRadius = new Windows.UI.Xaml.CornerRadius(5);
            Close.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            Children.Add(Close);

            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
            Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void Show(BarcodeResult result)
        {
            Visibility = Windows.UI.Xaml.Visibility.Visible;

            string text = "";
            foreach(var item in result.Barcodes)
            {
                text += item.Text + " (" + EnumExtensions.ToDescription(item.Type) + "), ";
            }
            TextBlock.Text = text.TrimEnd(' ').TrimEnd(',');
        }

        public void Hide()
        {
            Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}

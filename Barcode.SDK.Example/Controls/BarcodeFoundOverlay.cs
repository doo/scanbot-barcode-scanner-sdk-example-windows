using Scanbot.Model;
using Scanbot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace BarcodeSDK.Example.Controls
{
    public class BarcodeFoundOverlay : StackPanel
    {
        TextBlock TextBlock { get; set; }
        
        public Button Continue { get; private set; }
        public Button Close { get; private set; }

        public BarcodeFoundOverlay()
        {
            TextBlock = new TextBlock();
            TextBlock.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            TextBlock.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            TextBlock.FontSize = 20.0;
            TextBlock.Foreground = new SolidColorBrush(Colors.White);
            TextBlock.FontWeight = Windows.UI.Text.FontWeights.Bold;
            TextBlock.Margin = new Windows.UI.Xaml.Thickness(0, 0, 0, 100);
            Children.Add(TextBlock);

            Continue = new Button();
            Continue.Content = "Continue";
            Continue.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            Continue.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Continue.FontSize = 18.0;
            Continue.Foreground = new SolidColorBrush(Colors.White);
            Continue.Background = new SolidColorBrush(Colors.Blue);
            Continue.Margin = new Windows.UI.Xaml.Thickness(0, 0, 0, 0);
            Children.Add(Continue);

            Close = new Button();
            Close.Content = "Close";
            Close.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            Close.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Close.Background = new SolidColorBrush(Colors.Orange);
            Close.Margin = new Windows.UI.Xaml.Thickness(0, 15, 0, 0);
            Close.FontSize = 16.0;
            Close.Foreground = new SolidColorBrush(Colors.White);
            Children.Add(Close);

            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void Show(BarcodeResult result)
        {
            Visibility = Windows.UI.Xaml.Visibility.Visible;

            string text = "";
            foreach(var item in result.Barcodes)
            {
                text += item.Text + " (" + item.Type.Description() + "), ";
            }
            TextBlock.Text = text.TrimEnd(' ').TrimEnd(',');
        }

        public void Hide()
        {
            Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}

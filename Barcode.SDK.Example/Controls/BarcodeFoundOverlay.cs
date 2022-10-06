using Scanbot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

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
            Children.Add(TextBlock);

            Continue = new Button();
            Continue.Content = "Continue";
            Continue.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            Continue.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Children.Add(Continue);

            Close = new Button();
            Close.Content = "Close";
            Close.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            Close.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Children.Add(Close);

            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void Show(BarcodeResult result)
        {
            Visibility = Windows.UI.Xaml.Visibility.Visible;

            TextBlock.Text = result.ToString();
        }

        public void Hide()
        {
            Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}

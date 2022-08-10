
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Notifications;

namespace Barcode.SDK.Example.Utils
{
    public class Toast
    {
        static readonly ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
        static ToastNotification Current;
        static string Text;

        public static void Show(List<Barcode> barcodes)
        {
            if (barcodes.Count == 1)
            {
                Show(barcodes[0]);
                return;
            }

            var types = string.Join(", ", barcodes.Select(code => code.Type.Description()));
            Show(types, "Detected multiple barcodes!");
        }

        public static void Show(Barcode barcode)
        {
            Show(barcode.Text, "Detected " + barcode.Type);
        }

        public static void Show(string body, string title = "Barcode Detected!")
        {
            if (Text == body)
            {
                return;
            }

            if (Current != null)
            {
                ToastNotifier.Hide(Current);
            }

            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(body));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            Current = new ToastNotification(toastXml);
            Current.ExpirationTime = DateTime.Now.AddSeconds(2);
            ToastNotifier.Show(Current);
            Text = body;

            Current.Dismissed += OnHide;
        }

        public static void OnHide(ToastNotification sender, ToastDismissedEventArgs args)
        {
            Current.Dismissed -= OnHide;
            Text = null;
        }
    }
}

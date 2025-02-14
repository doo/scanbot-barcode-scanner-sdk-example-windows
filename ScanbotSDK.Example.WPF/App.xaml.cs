using ScanbotSDK.Barcode;
using ScanbotSDK.Common;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ScanbotSDK.Example.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /*
         * A trial license key is required for evaluation or testing!
         * You can get a free "no-strings-attached" trial license.
         * Please use the trial license form on our website: https://scanbot.io/trial/
         * You will need to provide the following package ID of this
         * example app: "io.scanbot.example.sdk.barcode.windows"
         */
        private const string LICENSE_KEY = ""; // TODO Insert your trial license key here

        public App()
        {
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            MediaFoundation.Init();
            BarcodeSDK.Initialize(LICENSE_KEY);
        }
    }
}

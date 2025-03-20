using ScanbotSDK.Barcode;
using ScanbotSDK.Common;
using System.ComponentModel;

namespace ScanbotSDK.Example.WinForms
{
    internal static class Program
    {
        /*
         * A trial license key is required for evaluation or testing!
         * You can get a free "no-strings-attached" trial license.
         * Please use the trial license form on our website: https://scanbot.io/trial/
         * You will need to provide the following package ID of this
         * example app: "io.scanbot.example.sdk.barcode.windows"
         */
        private const string LICENSE_KEY = ""; // TODO Insert your trial license key here

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Required for the CameraPreview control
            MediaFoundation.Init();
            BarcodeSDK.Initialize(LICENSE_KEY);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.SetColorMode(SystemColorMode.System);
            Application.Run(new Form1());
        }
    }
}
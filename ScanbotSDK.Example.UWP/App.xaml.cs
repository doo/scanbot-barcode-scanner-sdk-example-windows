using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Barcode.SDK.Example
{
    sealed partial class App : Application
    {
        /*
         * A trial license key is required for evaluation or testing!
         * You can get a free "no-strings-attached" trial license.
         * Please use the trial license form on our website: https://scanbot.io/trial/
         * You will need to provide the following package ID of this
         * example app: "io.scanbot.example.sdk.barcode.windows"
         */
        public const string LICENSE_KEY = ""; // TODO Insert your trial license key here

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                Window.Current.Content = rootFrame;
            }

            var license = ScanbotSDK.Barcode.BarcodeSDK.Initialize(LICENSE_KEY);

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                
                Window.Current.Activate();
            }
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}

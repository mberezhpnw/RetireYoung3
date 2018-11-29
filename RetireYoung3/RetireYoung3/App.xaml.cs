using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RetireYoung3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new GreetingPage()) { BarBackgroundColor = Color.FromHex("#607D8B") };

            if (Device.OS == TargetPlatform.Android)
            
                MainPage = new NavigationPage(new GreetingPage()) { BarBackgroundColor = Color.FromHex("#607D8B") };

            
            else if (Device.OS == TargetPlatform.iOS)
            
                MainPage = new NavigationPage(new GreetingPage()) { BarBackgroundColor = Color.FromHex("#607D8B") };
                
            

        }

        private void SetCultureToUSEnglish()
        {
            CultureInfo englishUSCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

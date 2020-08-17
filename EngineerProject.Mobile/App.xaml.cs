using EngineerProject.Mobile.Utility;
using EngineerProject.Mobile.Views;
using EngineerProject.Mobile.Views.Home;
using Xamarin.Forms;

namespace EngineerProject.Mobile
{
    public partial class App : Application
    {
        private static Page mainPage;

        public App()
        {
            if (Current.Properties.ContainsKey("token"))
            {
                ConfigurationData.IsUserLoggedIn = true;
                ConfigurationData.Token = Current.Properties["token"] as string;
            }

            if (!ConfigurationData.IsUserLoggedIn)
                MainPage = new NavigationPage(new LoginPage());
            else
                MainPage = new NavigationPage(new HomePage());

            mainPage = MainPage;
        }

        internal static void LogoutUser()
        {
            ConfigurationData.IsUserLoggedIn = false;
            ConfigurationData.Token = null;

            Current.Properties.Remove("token");

            mainPage = new NavigationPage(new LoginPage());
        }
    }
}
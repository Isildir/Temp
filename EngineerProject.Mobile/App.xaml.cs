using EngineerProject.Mobile.Utility;
using EngineerProject.Mobile.Views;
using Xamarin.Forms;

namespace EngineerProject.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            if (!ConfigurationData.IsUserLoggedIn)
                MainPage = new NavigationPage(new LoginPage());
            else
                MainPage = new NavigationPage(new HomePage());
        }
    }
}
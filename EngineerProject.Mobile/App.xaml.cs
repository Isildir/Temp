using EngineerProject.Mobile.Views;
using Xamarin.Forms;

namespace EngineerProject.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            if (!IsUserLoggedIn)
                MainPage = new NavigationPage(new LoginPage());
            else
                MainPage = new NavigationPage(new MainPage());
        }

        public static bool IsUserLoggedIn { get; internal set; }

        public static string Token { get; internal set; }

        protected override void OnResume()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnStart()
        {
        }
    }
}
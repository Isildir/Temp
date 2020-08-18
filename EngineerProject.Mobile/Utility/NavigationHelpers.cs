using EngineerProject.Mobile.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Utility
{
    public static class NavigationHelpers
    {
        public static async Task LogoutUser()
        {
            ConfigurationData.IsUserLoggedIn = false;
            ConfigurationData.Token = null;

            await ApplicationPropertiesHandler.RemoveProperty("token");

            var navigation = Application.Current.MainPage.Navigation;

            await navigation.PopToRootAsync();
            await navigation.PushAsync(new LoginPage());
        }
    }
}
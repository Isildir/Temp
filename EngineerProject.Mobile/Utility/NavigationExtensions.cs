using EngineerProject.Mobile.Utility;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home
{
    public static class NavigationExtensions
    {
        public static async Task OnLogout(this ContentPage parent)
        {
            ConfigurationData.IsUserLoggedIn = false;
            ConfigurationData.Token = null;

            parent.Navigation.InsertPageBefore(new LoginPage(), parent);
            await parent.Navigation.PopAsync();
        }
    }
}
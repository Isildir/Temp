using EngineerProject.Commons.Dtos;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Views.Home.Pages;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace EngineerProject.Mobile.Views.Home
{
    public partial class HomePage : Xamarin.Forms.TabbedPage
    {
        internal static UserGroupsWrapperDto UserData = new UserGroupsWrapperDto();

        public HomePage()
        {
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            InitializeComponent();

            HomeWrapper.Children.Add(new UserGroupsPage());
            HomeWrapper.Children.Add(new SearchPage());
            HomeWrapper.Children.Add(new InvitesPage());
        }

        internal static async Task ReloadUserData()
        {
            var service = new HomeService();

            var userGroups = await service.GetUserGroups();

            if (userGroups.IsSuccessful)
                UserData = userGroups.Data;
        }
    }
}
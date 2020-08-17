using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Views.Home.CarouselComponents;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home
{
    public partial class HomePage : CarouselPage
    {
        internal static UserGroupsWrapperDto UserData = new UserGroupsWrapperDto();

        public HomePage()
        {
            InitializeComponent();

            HomeWrapper.Children.Add(new UserGroups());
            HomeWrapper.Children.Add(new GroupSearch());
            HomeWrapper.Children.Add(new GroupInvites());
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
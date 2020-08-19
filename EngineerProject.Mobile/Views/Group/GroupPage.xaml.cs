using EngineerProject.Mobile.Views.Group.Components;
using EngineerProject.Mobile.Views.Group.Pages;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace EngineerProject.Mobile.Views.Group
{
    public partial class GroupPage : Xamarin.Forms.TabbedPage
    {
        public static int GroupId;

        public GroupPage(int groupId, string groupName, bool isOwner)
        {
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            GroupId = groupId;

            InitializeComponent();

            Title = groupName;

            GroupWrapper.Children.Add(new PostsPage());
            GroupWrapper.Children.Add(new ChatPage());
            GroupWrapper.Children.Add(new FilesPage());

            if (isOwner)
                GroupWrapper.Children.Add(new AdminDetailsPage());
            else
                GroupWrapper.Children.Add(new UserDetailsPage());
        }
    }
}
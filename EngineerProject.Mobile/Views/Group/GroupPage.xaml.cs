using EngineerProject.Mobile.Views.Group.Components;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace EngineerProject.Mobile.Views.Group
{
    public partial class GroupPage : Xamarin.Forms.TabbedPage
    {
        public static int GroupId;

        public GroupPage(int id)
        {
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            GroupId = id;

            InitializeComponent();

            GroupWrapper.Children.Add(new PostsPage());
            GroupWrapper.Children.Add(new ChatPage());
            GroupWrapper.Children.Add(new FilesPage());
        }
    }
}
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Groups
{
    public partial class GroupPage : ContentPage
    {
        public GroupPage(int id)
        {
            InitializeComponent();

            Id.Text = id.ToString();
        }
    }
}
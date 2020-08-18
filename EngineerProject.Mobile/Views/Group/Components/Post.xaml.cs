using EngineerProject.Commons.Dtos.Groups;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Components
{
    public partial class Post : StackLayout
    {
        public Post(PostDto data)
        {
            Data = data;

            InitializeComponent();

            BindingContext = this;
        }

        public PostDto Data { get; set; }
    }
}
using EngineerProject.Commons.Dtos;
using EngineerProject.Mobile.Utility;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Components
{
    public partial class Message : Frame
    {
        public Message(MessageDto data)
        {
            InitializeComponent();

            Owner.Text = data.Owner;
            Content.Text = data.Content;
            DateAdded.Text = data.DateAdded.DateToString();
        }
    }
}
using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using System;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Components
{
    public partial class Comment : Frame
    {
        private readonly Action<Comment> deleteAction;
        private readonly int id;

        public Comment(CommentDto data, Action<Comment> deleteAction)
        {
            this.id = data.Id;
            this.deleteAction = deleteAction;

            InitializeComponent();

            Owner.Text = data.Owner;
            Content.Text = data.Content;
            DateAdded.Text = data.DateAdded.DateToString();
            CommentDeleteButton.IsVisible = data.IsOwner;
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            var service = new GroupService();

            var requestResponse = await service.DeleteComment(id);

            if (requestResponse.IsSuccessful)
                deleteAction.Invoke(this);
        }
    }
}
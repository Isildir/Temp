using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using System;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Components
{
    public partial class Post : Frame
    {
        private readonly int postId;

        public Post(PostDto data)
        {
            postId = data.Id;

            InitializeComponent();

            Owner.Text = data.Owner;
            OldTitle.Text = data.Title;
            OldContent.Text = data.Content;
            DateAdded.Text = data.DateAdded.DateToString();

            EditButton.IsVisible = data.IsOwner;

            foreach (var comment in data.Comments)
                CommentsList.Children.Add(new Comment(comment, OnCommentDelete));
        }

        private void OnCommentDelete(Comment comment) => CommentsList.Children.Remove(comment);

        private async void OnCommentSubmit(object sender, EventArgs e)
        {
            var service = new GroupService();

            var requestResult = await service.AddComment(postId, NewComment.Text);

            if (requestResult.IsSuccessful)
            {
                CommentsList.Children.Add(new Comment(requestResult.Data, OnCommentDelete));

                NewComment.Text = string.Empty;
            }
        }

        private void OnEditionAbort(object sender, EventArgs e)
        {
            NewPostTitle.Text = OldTitle.Text;
            NewPostContent.Text = OldContent.Text;

            SetElementsVisibility(false);
        }

        private async void OnEditionConfirm(object sender, EventArgs e)
        {
            var service = new GroupService();

            var requestResult = await service.ModifyPost(postId, NewPostTitle.Text, NewPostContent.Text);

            if (requestResult.IsSuccessful)
            {
                OldTitle.Text = NewPostTitle.Text;
                OldContent.Text = NewPostContent.Text;

                SetElementsVisibility(false);
            }
        }

        private void OnPostEdit(object sender, EventArgs e)
        {
            SetElementsVisibility(true);
        }

        private void SetElementsVisibility(bool edition)
        {
            DisplayDataView.IsVisible = !edition;
            EditDataView.IsVisible = edition;

            AbortButton.IsVisible = edition;
            ConfirmButton.IsVisible = edition;
            EditButton.IsVisible = !edition;
        }
    }
}
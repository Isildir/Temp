using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Components
{
    public partial class Post : StackLayout
    {
        private readonly Dictionary<int, StackLayout> CommentsReferenceCache = new Dictionary<int, StackLayout>();
        private string content;
        private string title;

        public Post(PostDto data)
        {
            PostId = data.Id;
            IsOwner = data.IsOwner;
            Owner = data.Owner;
            DateAdded = FormatDate(data.DateAdded);

            InitializeComponent();

            Title = data.Title;
            Content = data.Content;

            BindingContext = this;

            foreach (var comment in data.Comments)
                AddComment(comment);
        }

        public string Content
        {
            get { return content; }
            set
            {
                OldContent.Text = value;
                NewPostContent.Text = value;
                content = value;
            }
        }

        public string DateAdded { get; set; }

        public bool IsOwner { get; set; }

        public string Owner { get; set; }

        public int PostId { get; set; }

        public string Title
        {
            get { return title; }
            set
            {
                OldTitle.Text = value;
                NewPostTitle.Text = value;
                title = value;
            }
        }

        private void AddComment(CommentDto data)
        {
            var layout = new StackLayout();
            var secondLayer = new StackLayout { Orientation = StackOrientation.Horizontal };

            secondLayer.Children.Add(new Label
            {
                Text = data.Owner,
                HorizontalOptions = LayoutOptions.StartAndExpand
            });
            secondLayer.Children.Add(new Label
            {
                Text = FormatDate(data.DateAdded),
                HorizontalOptions = LayoutOptions.End
            });

            layout.Children.Add(new Label { Text = data.Content });
            layout.Children.Add(secondLayer);

            if (data.IsOwner)
            {
                var deleteButton = new Button
                {
                    Text = "Usuń",
                    CommandParameter = data.Id,
                    HorizontalOptions = LayoutOptions.End
                };

                deleteButton.Clicked += OnCommentDelete;

                layout.Children.Add(deleteButton);
            }

            CommentsList.Children.Add(layout);
            CommentsReferenceCache.Add(data.Id, layout);
        }

        private string FormatDate(DateTime date) => $"{date.ToShortDateString()} {date.ToShortTimeString()}";

        private async void OnCommentAdded(object sender, EventArgs e)
        {
            var service = new GroupService();

            var requestResult = await service.AddComment(PostId, NewComment.Text);

            if (requestResult.IsSuccessful)
            {
                AddComment(requestResult.Data);

                NewComment.Text = string.Empty;
            }
        }

        private async void OnCommentDelete(object sender, EventArgs e)
        {
            var commentId = (int)(sender as Button).CommandParameter;

            var service = new GroupService();

            var requestResponse = await service.DeleteComment(commentId);

            if (requestResponse.IsSuccessful)
            {
                var reference = CommentsReferenceCache[commentId];

                CommentsReferenceCache.Remove(commentId);
                CommentsList.Children.Remove(reference);
            }
        }

        private void OnEditionAbort(object sender, EventArgs e)
        {
            Title = title;
            Content = content;

            SetElementsVisibility(false);
        }

        private async void OnEditionConfirm(object sender, EventArgs e)
        {
            var service = new GroupService();

            var requestResult = await service.ModifyPost(PostId, NewPostTitle.Text, NewPostContent.Text);

            if (requestResult.IsSuccessful)
            {
                Title = NewPostTitle.Text;
                Content = NewPostContent.Text;

                SetElementsVisibility(false);
            }
        }

        private void OnPostEdit(object sender, EventArgs e)
        {
            SetElementsVisibility(true);
        }

        private void SetElementsVisibility(bool edition)
        {
            OldTitle.IsVisible = !edition;
            OldContent.IsVisible = !edition;
            NewPostTitle.IsVisible = edition;
            NewPostContent.IsVisible = edition;

            AbortButton.IsVisible = edition;
            ConfirmButton.IsVisible = edition;
            EditButton.IsVisible = !edition;
        }
    }
}
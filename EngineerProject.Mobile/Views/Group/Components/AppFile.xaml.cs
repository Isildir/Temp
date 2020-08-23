using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Utility;
using System;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Components
{
    public partial class AppFile : Frame
    {
        public AppFile(FileDto data, Action<object, EventArgs> onFileClickedAction)
        {
            InitializeComponent();

            FileName.Text = data.FileName;
            Size.Text = data.Size;
            Src.Source = ResolveIconSource(data.FileName);
            DateAdded.Text = data.DateAdded.DateToString();

            var tapGesture = new TapGestureRecognizer { CommandParameter = data };

            tapGesture.Tapped += (sender, args) => onFileClickedAction(sender, args);

            GestureRecognizers.Add(tapGesture);
        }

        private string ResolveIconSource(string fileName)
        {
            return "icon.png";
        }
    }
}
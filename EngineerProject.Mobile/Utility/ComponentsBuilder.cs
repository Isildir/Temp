using EngineerProject.Mobile.Components;
using System;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Utility
{
    public static class ComponentsBuilder
    {
        public static Frame BuildAcceptableFrame(
            string labelText,
            object buttonData,
            Action<object, EventArgs> acceptMethod,
            Action<object, EventArgs> rejectMethod,
            string acceptButtonText = "Zaakceptuj",
            string rejectButtonText = "Odrzuć")
        {
            var layout = new StackLayout { Orientation = StackOrientation.Horizontal };
            var label = new Label { Text = labelText, HorizontalOptions = LayoutOptions.StartAndExpand, FontSize = NamedSize.Medium.GetFormattedLabelFontSize() };
            var acceptButton = new AppButton { Text = acceptButtonText, HorizontalOptions = LayoutOptions.EndAndExpand, BackgroundColor = Color.Transparent };
            var rejectButton = new AppButton { Text = rejectButtonText, BackgroundColor = Color.Transparent };
            var acceptGesture = new TapGestureRecognizer { CommandParameter = buttonData };
            var rejectGesture = new TapGestureRecognizer { CommandParameter = buttonData };
            var frame = new Frame { Content = layout, BackgroundColor = Color.WhiteSmoke, Padding = 6, Margin = 2 };

            acceptGesture.Tapped += (sender, args) => acceptMethod(sender, args);
            rejectGesture.Tapped += (sender, args) => rejectMethod(sender, args);

            acceptButton.GestureRecognizers.Add(acceptGesture);
            rejectButton.GestureRecognizers.Add(rejectGesture);

            layout.Children.Add(label);
            layout.Children.Add(acceptButton);
            layout.Children.Add(rejectButton);

            return frame;
        }
    }
}
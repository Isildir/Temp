using Xamarin.Forms;

namespace EngineerProject.Mobile.Utility
{
    public static class FontsHelpers
    {
        public static double GetFormattedLabelFontSize(this NamedSize size) => Device.GetNamedSize(size, typeof(Label));
    }
}
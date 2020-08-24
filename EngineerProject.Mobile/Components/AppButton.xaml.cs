using Xamarin.Forms;

namespace EngineerProject.Mobile.Components
{
    public partial class AppButton : Frame
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(AppButton), default(string), BindingMode.OneWay, propertyChanged: OnTextpropertyChanged);

        public AppButton()
        {
            InitializeComponent();
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        private static void OnTextpropertyChanged(BindableObject bindableObject, object oldValue, object newValue) => (bindableObject as AppButton).ButtonText.Text = (string)newValue;
    }
}
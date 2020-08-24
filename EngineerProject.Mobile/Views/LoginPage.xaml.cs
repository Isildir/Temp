using EngineerProject.Mobile.Components;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using EngineerProject.Mobile.Views.Home;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly Dictionary<ElementType, View> preparedElements = new Dictionary<ElementType, View>();

        private FormState currentFormState = FormState.Login;

        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            PrepareGridElements();
            UpdateGridShape();
        }

        private enum ElementType : byte
        {
            Identifier = 1,
            Login = 2,
            Email = 3,
            Password = 4,
            ConfirmedPassword = 5,
            LoginLabel = 6,
            RegisterLabel = 7,
            RecoveryLabel = 8,
            SubmitButton = 9,
            ErrorLabel = 10
        }

        private enum FormState : byte
        {
            Login = 1,
            Register = 2,
            PasswordRecovery = 3
        }

        private void AppendLoginControls()
        {
            preparedElements[ElementType.Identifier].IsVisible = true;
            preparedElements[ElementType.Password].IsVisible = true;
            preparedElements[ElementType.RegisterLabel].IsVisible = true;
            preparedElements[ElementType.RecoveryLabel].IsVisible = true;
        }

        private void AppendRecoveryControls()
        {
            preparedElements[ElementType.Identifier].IsVisible = true;
            preparedElements[ElementType.LoginLabel].IsVisible = true;
        }

        private void AppendRegisterControls()
        {
            preparedElements[ElementType.Login].IsVisible = true;
            preparedElements[ElementType.Email].IsVisible = true;
            preparedElements[ElementType.Password].IsVisible = true;
            preparedElements[ElementType.ConfirmedPassword].IsVisible = true;
            preparedElements[ElementType.LoginLabel].IsVisible = true;
        }

        private async Task<string> HandleLogging()
        {
            var identifier = ((Entry)preparedElements[ElementType.Identifier]).Text;
            var password = ((Entry)preparedElements[ElementType.Password]).Text;

            var request = await new UserService().Login(identifier, password);

            if (request.IsSuccessful)
            {
                ConfigurationData.IsUserLoggedIn = true;
                ConfigurationData.Token = request.Data;

                await ApplicationPropertiesHandler.AddProperty("token", request.Data);

                Navigation.InsertPageBefore(new HomePage(), this);
                await Navigation.PopAsync();
            }

            return request.ErrorMessage;
        }

        private async Task<string> HandlePasswordRecovery()
        {
            var identifier = ((Entry)preparedElements[ElementType.Identifier]).Text;

            var request = await new UserService().SendPasswordRecovery(identifier);

            if (request.IsSuccessful)
            {
                currentFormState = FormState.Login;
                UpdateGridShape();

                return "Email został wysłany";
            }

            return request.ErrorMessage;
        }

        private async Task<string> HandleRegistering()
        {
            var password = ((Entry)preparedElements[ElementType.Password]).Text;
            var confirmedPassword = ((Entry)preparedElements[ElementType.ConfirmedPassword]).Text;

            if (!password.Equals(confirmedPassword))
                return "Hasła nie są identyczne";

            var login = ((Entry)preparedElements[ElementType.Login]).Text;
            var email = ((Entry)preparedElements[ElementType.Email]).Text;

            var request = await new UserService().Register(login, email, password);

            if (request.IsSuccessful)
            {
                currentFormState = FormState.Login;
                UpdateGridShape();

                return "Rejestracja powiodła się, możesz się teraz zalogować";
            }

            return request.ErrorMessage;
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            currentFormState = FormState.Login;
            UpdateGridShape();
        }

        private void OnPasswordRecoveryButtonClicked(object sender, EventArgs e)
        {
            currentFormState = FormState.PasswordRecovery;
            UpdateGridShape();
        }

        private void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            currentFormState = FormState.Register;
            UpdateGridShape();
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            preparedElements[ElementType.ErrorLabel].IsVisible = false;

            string errorMessage = null;

            switch (currentFormState)
            {
                case FormState.Login:
                    errorMessage = await HandleLogging();
                    break;

                case FormState.Register:
                    errorMessage = await HandleRegistering();
                    break;

                case FormState.PasswordRecovery:
                    errorMessage = await HandlePasswordRecovery();
                    break;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ((Label)preparedElements[ElementType.ErrorLabel]).Text = errorMessage;
                preparedElements[ElementType.ErrorLabel].IsVisible = true;
            }
        }

        private void PrepareGridElements()
        {
            var secondaryButtonStyles = new List<string> { "style-test" };

            var loginLabel = new Label { Text = "Logowanie", StyleClass = secondaryButtonStyles, HorizontalOptions = LayoutOptions.StartAndExpand };
            var registerLabel = new Label { Text = "Rejestracja", StyleClass = secondaryButtonStyles, HorizontalOptions = LayoutOptions.StartAndExpand };
            var recoveryLabel = new Label { Text = "Przypomnienie hasła", StyleClass = secondaryButtonStyles, HorizontalOptions = LayoutOptions.StartAndExpand };

            var submitButton = new AppButton { Text = "Zatwierdź" };

            var loginGesture = new TapGestureRecognizer();
            var registerGesture = new TapGestureRecognizer();
            var recoveryGesture = new TapGestureRecognizer();
            var submitGesture = new TapGestureRecognizer();

            loginGesture.Tapped += OnLoginButtonClicked;
            registerGesture.Tapped += OnRegisterButtonClicked;
            recoveryGesture.Tapped += OnPasswordRecoveryButtonClicked;
            submitGesture.Tapped += OnSubmitButtonClicked;

            loginLabel.GestureRecognizers.Add(loginGesture);
            registerLabel.GestureRecognizers.Add(registerGesture);
            recoveryLabel.GestureRecognizers.Add(recoveryGesture);
            submitButton.GestureRecognizers.Add(submitGesture);

            preparedElements.Add(ElementType.Identifier, new Entry { Placeholder = "Identyfikator" });
            preparedElements.Add(ElementType.Login, new Entry { Placeholder = "Login" });
            preparedElements.Add(ElementType.Email, new Entry { Placeholder = "Email" });
            preparedElements.Add(ElementType.Password, new Entry { Placeholder = "Hasło", IsPassword = true });
            preparedElements.Add(ElementType.ConfirmedPassword, new Entry { Placeholder = "Powtórz hasło", IsPassword = true });
            preparedElements.Add(ElementType.LoginLabel, loginLabel);
            preparedElements.Add(ElementType.RegisterLabel, registerLabel);
            preparedElements.Add(ElementType.RecoveryLabel, recoveryLabel);
            preparedElements.Add(ElementType.SubmitButton, submitButton);
            preparedElements.Add(ElementType.ErrorLabel, new Label());

            var buttonsLayout = new StackLayout { Orientation = StackOrientation.Horizontal };

            buttonsLayout.Children.Add(preparedElements[ElementType.LoginLabel]);
            buttonsLayout.Children.Add(preparedElements[ElementType.RegisterLabel]);
            buttonsLayout.Children.Add(preparedElements[ElementType.RecoveryLabel]);

            GridLayout.Children.Add(preparedElements[ElementType.Identifier]);
            GridLayout.Children.Add(preparedElements[ElementType.Login]);
            GridLayout.Children.Add(preparedElements[ElementType.Email]);
            GridLayout.Children.Add(preparedElements[ElementType.Password]);
            GridLayout.Children.Add(preparedElements[ElementType.ConfirmedPassword]);
            GridLayout.Children.Add(buttonsLayout);
            GridLayout.Children.Add(preparedElements[ElementType.SubmitButton]);
            GridLayout.Children.Add(preparedElements[ElementType.ErrorLabel]);
        }

        private void UpdateGridShape()
        {
            foreach (var element in preparedElements)
                element.Value.IsVisible = false;

            switch (currentFormState)
            {
                case FormState.Login:
                    AppendLoginControls();
                    break;

                case FormState.Register:
                    AppendRegisterControls();
                    break;

                case FormState.PasswordRecovery:
                    AppendRecoveryControls();
                    break;
            }

            preparedElements[ElementType.SubmitButton].IsVisible = true;
        }
    }
}
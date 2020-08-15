using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views
{
    public partial class LoginPage : ContentPage
    {
        private enum FormState : byte
        {
            Login = 1,
            Register = 2,
            PasswordRecovery = 3
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

        private FormState currentFormState = FormState.Login;
        private readonly Dictionary<ElementType, View> preparedElements = new Dictionary<ElementType, View>();

        private void PrepareGridElements()
        {
            var secondaryButtonStyles = new List<string> { "style-test" };

            var loginLabel = new Label { Text = "Logowanie", StyleClass = secondaryButtonStyles };
            var registerLabel = new Label { Text = "Rejestracja", StyleClass = secondaryButtonStyles };
            var recoveryLabel = new Label { Text = "Przypomnienie hasła", StyleClass = secondaryButtonStyles };

            var loginGesture = new TapGestureRecognizer();
            var registerGesture = new TapGestureRecognizer();
            var recoveryGesture = new TapGestureRecognizer();

            loginGesture.Tapped += OnLoginButtonClicked;
            registerGesture.Tapped += OnRegisterButtonClicked;
            recoveryGesture.Tapped += OnPasswordRecoveryButtonClicked;

            loginLabel.GestureRecognizers.Add(loginGesture);
            registerLabel.GestureRecognizers.Add(registerGesture);
            recoveryLabel.GestureRecognizers.Add(recoveryGesture);

            preparedElements.Add(ElementType.Identifier, new Entry { Placeholder = "identyfikator" });
            preparedElements.Add(ElementType.Login, new Entry { Placeholder = "login" });
            preparedElements.Add(ElementType.Email, new Entry { Placeholder = "email" });
            preparedElements.Add(ElementType.Password, new Entry { Placeholder = "hasło", IsPassword = true });
            preparedElements.Add(ElementType.ConfirmedPassword, new Entry { Placeholder = "powtórz hasło", IsPassword = true });
            preparedElements.Add(ElementType.LoginLabel, loginLabel);
            preparedElements.Add(ElementType.RegisterLabel, registerLabel);
            preparedElements.Add(ElementType.RecoveryLabel, recoveryLabel);
            preparedElements.Add(ElementType.SubmitButton, new Button { Text = "Zatwierdź" });
            preparedElements.Add(ElementType.ErrorLabel, new Label());

            ((Button)preparedElements[ElementType.SubmitButton]).Clicked += OnSubmitButtonClicked;
        }

        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            PrepareGridElements();
            InitializeComponent();
            UpdateGridShape();
        }

        private void AppendLoginControls(ref int rowIndex)
        {
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 50 });
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 50 });
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 20 });

            gridLayout.Children.Add(preparedElements[ElementType.Identifier], 0, rowIndex++);
            gridLayout.Children.Add(preparedElements[ElementType.Password], 0, rowIndex++);
            gridLayout.Children.Add(preparedElements[ElementType.RegisterLabel], 0, rowIndex);
            gridLayout.Children.Add(preparedElements[ElementType.RecoveryLabel], 1, rowIndex);

            Grid.SetColumnSpan(preparedElements[ElementType.Identifier], 2);
            Grid.SetColumnSpan(preparedElements[ElementType.Password], 2);
        }

        private void AppendRegisterControls(ref int rowIndex)
        {
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 50 });
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 50 });
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 50 });
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 50 });
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 20 });

            gridLayout.Children.Add(preparedElements[ElementType.Login], 0, rowIndex++);
            gridLayout.Children.Add(preparedElements[ElementType.Email], 0, rowIndex++);
            gridLayout.Children.Add(preparedElements[ElementType.Password], 0, rowIndex++);
            gridLayout.Children.Add(preparedElements[ElementType.ConfirmedPassword], 0, rowIndex++);
            gridLayout.Children.Add(preparedElements[ElementType.LoginLabel], 0, rowIndex);

            Grid.SetColumnSpan(preparedElements[ElementType.Login], 2);
            Grid.SetColumnSpan(preparedElements[ElementType.Email], 2);
            Grid.SetColumnSpan(preparedElements[ElementType.Password], 2);
            Grid.SetColumnSpan(preparedElements[ElementType.ConfirmedPassword], 2);
        }

        private void AppendRecoveryControls(ref int rowIndex)
        {
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 50 });
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 20 });

            gridLayout.Children.Add(preparedElements[ElementType.Identifier], 0, rowIndex++);
            gridLayout.Children.Add(preparedElements[ElementType.LoginLabel], 0, rowIndex);

            Grid.SetColumnSpan(preparedElements[ElementType.Identifier], 2);
        }

        private void UpdateGridShape()
        {
            gridLayout.Children.Clear();
            gridLayout.RowDefinitions.Clear();

            var rowIndex = 0;

            switch (currentFormState)
            {
                case FormState.Login:
                    AppendLoginControls(ref rowIndex);
                    break;
                case FormState.Register:
                    AppendRegisterControls(ref rowIndex);
                    break;
                case FormState.PasswordRecovery:
                    AppendRecoveryControls(ref rowIndex);
                    break;
            }

            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 50 });
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = 50 });

            gridLayout.Children.Add(preparedElements[ElementType.SubmitButton], 1, ++rowIndex);
            gridLayout.Children.Add(preparedElements[ElementType.ErrorLabel], 0, ++rowIndex);
            Grid.SetColumnSpan(preparedElements[ElementType.ErrorLabel], 2);

            gridLayout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
        }

        private void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            currentFormState = FormState.Register;
            UpdateGridShape();
        }
        private void OnPasswordRecoveryButtonClicked(object sender, EventArgs e)
        {
            currentFormState = FormState.PasswordRecovery;
            UpdateGridShape();
        }
        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            currentFormState = FormState.Login;
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

                return "Rejestracja powiodła się, możesz sięteraz zalogować";
            }

            return request.ErrorMessage;
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

                Navigation.InsertPageBefore(new HomePage(), this);
                await Navigation.PopAsync();
            }

            return request.ErrorMessage;
        }
    }
}
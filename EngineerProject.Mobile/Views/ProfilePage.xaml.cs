using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.ViewModels;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views
{

    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            CharactersView.ItemsSource = Characters;

            LoadCharactersData();
        }

        public ObservableCollection<Group> Characters { get; set; } = new ObservableCollection<Group>();

        private async Task LoadCharactersData()
        {
            /*var values = await CharacterService.GetCharactersList();

            foreach (var value in values)
                Characters.Add(value);*/
        }

        private async void OnCharacterSelect(object sender, ItemTappedEventArgs e)
        {/*
            var t = e.Item as Character;

            await Navigation.PushAsync(new CharacterPage(t.Id));*/
        }

        private async void OnNewCharacterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterCreatePage());
        }
    }
}
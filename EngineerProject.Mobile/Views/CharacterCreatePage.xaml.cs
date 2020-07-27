using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views
{
    public partial class CharacterCreatePage : ContentPage
    {
        public CharacterCreatePage()
        {
            InitializeComponent();

            racePicker.ItemsSource = Races;
            professionPicker.ItemsSource = AvailableProfessions;

            LoadData();
        }

        public ObservableCollection<string> AvailableProfessions { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> Races { get; set; } = new ObservableCollection<string>();

        private void Button_Clicked(object sender, EventArgs e)
        {
        }

        private async Task LoadData()
        {/*
            creationData = await CharacterService.GetRaces();

            foreach (var race in creationData)
                Races.Add(race.RaceName);*/
        }

        //private List<ShortProfessionDto> professions = new List<ShortProfessionDto>();

        private async void ProfessionPickerIndexChanged(object sender, EventArgs e)
        {/*
            if (professionPicker.SelectedIndex == -1)
                return;

            var selectedProfessionId = professions[professionPicker.SelectedIndex].Id;

            selectedProfessionData = await CharacterService.GetProfessionData(selectedProfessionId);

            createButton.IsEnabled = true;

            UpdateLabelValue(professionSkillsSet, selectedProfessionData.SkillsSet.Select(a => a.Name), "Profession skills");
            UpdateLabelValue(professionAbilitiesSet, selectedProfessionData.AbilitiesSet.Select(a => a.Name), "Profession abilities");*/
        }

        private async void RacePickerIndexChanged(object sender, EventArgs e)
        {/*
            var selectedRace = creationData[racePicker.SelectedIndex];

            professions = await CharacterService.GetAvailableProfessions(selectedRace.Race);

            professionPicker.IsVisible = true;

            AvailableProfessions.Clear();

            professions.ForEach(a => AvailableProfessions.Add(a.Name));

            UpdateLabelValue(raceSkillsSet, selectedRace.SkillsSet.Select(a => a.Name), "Race skills");
            UpdateLabelValue(raceAbilitiesSet, selectedRace.AbilitiesSet.Select(a => a.Name), "Race abilities");*/
        }

        private void UpdateLabelValue(Label label, IEnumerable<string> values, string title)
        {
            if (values.Any())
            {
                var sb = new StringBuilder();

                sb.Append($"{title}:");

                foreach (var record in values)
                    sb.Append($" {record},");

                label.Text = sb.ToString().TrimEnd(',');

                label.IsVisible = true;
            }
            else
                raceSkillsSet.IsVisible = false;
        }
    }
}
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GroupProject_DD
{
    public partial class ScorePage : ContentPage
    {
        private PlayerController playerController;

        public ScorePage()
        {
            InitializeComponent();
            this.playerController = new PlayerController();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ScoreListView.ItemsSource = this.playerController.OrderedPlayers();
        }

        // directs to monster page
        async void newGameBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}

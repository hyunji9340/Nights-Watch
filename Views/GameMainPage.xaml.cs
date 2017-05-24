using GroupProject_DD.Models;
using GroupProject_DD.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GroupProject_DD
{
    public partial class GameMainPage : ContentPage
    {
        private Player currentPlayer;
        private Settings settings;


        public GameMainPage(Player currentPlayer)
        {
            InitializeComponent();
            this.currentPlayer = currentPlayer;
            settings = new Settings();
        }

        async void CharacterBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterPage());
        }

        async void MonsterBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MonsterPage());
        }

        async void StartGameBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameStartPage(currentPlayer, settings));
        }

        async void LeaderboardClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScorePage());
        }

        async void SettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(settings));
        }
        async void ServerItemClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ItemPage(settings));
		}
    }
}

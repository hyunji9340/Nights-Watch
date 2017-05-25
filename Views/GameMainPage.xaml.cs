using GroupProject_DD.Models;
using GroupProject_DD.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupProject_DD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameMainPage : ContentPage
    {
        private Player currentPlayer;
		public ServerItemController serverItemController; // added


		public GameMainPage(Player currentPlayer, ServerItemController serverItemController) // serverItemController added
        {
            InitializeComponent();
            this.currentPlayer = currentPlayer;
			this.serverItemController = serverItemController;
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
			await Navigation.PushAsync(new GameStartPage(currentPlayer, serverItemController));
        }

        async void LeaderboardClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScorePage());
        }

        async void SettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(serverItemController.currentSetting));
        }
        async void ServerItemClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ItemPage(serverItemController)); // serverItemController added, setting deleted
		}
    }
}

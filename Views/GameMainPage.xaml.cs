using GroupProject_DD.Models;
using GroupProject_DD.Views;
using GroupProject_DD.ViewModels;
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
        public BattleEffectController battleEffectCntrl;


		public GameMainPage(Player currentPlayer, ServerItemController serverItemController, BattleEffectController battleEffectCntrl) // serverItemController added
        {
            InitializeComponent();
            this.currentPlayer = currentPlayer;
			this.serverItemController = serverItemController;
            this.battleEffectCntrl = battleEffectCntrl;
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
            await Navigation.PushAsync(new GameStartPage(currentPlayer, serverItemController, battleEffectCntrl));
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

        async void BattleEffectClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ItemPage(battleEffectCntrl)); // serverItemController added, setting deleted
        }

        protected async override void OnAppearing()
        {
            await serverItemController.GetItemsAsync();
        }
    }
}

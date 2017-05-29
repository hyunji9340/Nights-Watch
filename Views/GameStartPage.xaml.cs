using GroupProject_DD.Models;
using GroupProject_DD.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GroupProject_DD
{
    public partial class GameStartPage : ContentPage
    {
        private CharacterController characterController;
        private CharacterDetailPage characterDetailPage;
        private Player currentPlayer;
		private ServerItemController serverItemController;
        private Settings settings;
        private BattleEffectController battleEffects;

		public GameStartPage(Player currentPlayer, ServerItemController serverItemController, BattleEffectController battleEffectCntrl) // setting deleted
        {
            InitializeComponent();
            this.characterController = new CharacterController();
            this.currentPlayer = currentPlayer;
			this.serverItemController = serverItemController;
            //settings = IncomingSettings;
            this.battleEffects = battleEffectCntrl;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = this.characterController;
        }

        // This method directs a detail page for a specific item
        async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var character = args.SelectedItem as Character;

            if (character == null)
                return;

            this.characterDetailPage = new CharacterDetailPage(new CharacterDetailViewModel(character));
            characterDetailPage.setCharacterController(characterController);

            await Navigation.PushAsync(this.characterDetailPage);

            // Manually deselect item
            CharacterListView.SelectedItem = null;
        }

        async void GoBtnClicked(object sender, System.EventArgs e)
        {
			await Navigation.PushAsync(new BattlePage(currentPlayer, serverItemController, battleEffects)); // setting deleted, added serverItemController
        }
    }
}

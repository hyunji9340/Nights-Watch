using GroupProject_DD.Models;
using GroupProject_DD.Views;
using GroupProject_DD.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;

namespace GroupProject_DD
{
    public partial class BattlePage : ContentPage
    {
        private BattleViewModel BattleEngineView;
        private int counter;
        Random rand;
        private CharacterStatsPage characterStatsPage;
        private CharacterController characterController;
        private Player currentPlayer;
        private Settings settings;
		public ServerItemController serverItemController;
        public BattleEffectController battleEffects;

        public BattlePage(Player currentPlayer, ServerItemController serverItemController, BattleEffectController battleEffects)
        {
            InitializeComponent();
            //settings = IncomingSettings;
            this.serverItemController = serverItemController;
            DependencyService.Get<IAudioPlayerService>().Pause();
            DependencyService.Get<IAudioPlayerService>().Play("prelude");
            this.currentPlayer = currentPlayer;
            BindingContext = BattleEngineView = new BattleViewModel(currentPlayer, serverItemController);
            counter = 0;
            rand = new Random();
            this.characterController = new CharacterController();
            this.battleEffects = battleEffects;

            var randomEvent = battleEffects.Effects[rand.Next(0, battleEffects.Effects.Count)].UseEffect(BattleEngineView.CharacterList, BattleEngineView.monster_dictionary);
            DisplayAlert("Battle Event!", randomEvent, "OK");
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            // need to check if a battle has ended
            if (BattleEngineView.isBattleEnded)
            {
                OnAlertYesNoClicked(this, null);
            }
            else
            {
                string battleSound = ".mp3";
                switch (rand.Next(1, 5))
                {
                    case 1:
                        battleSound = "battleA" + battleSound;
                        break;
                    case 2:
                        battleSound = "battleB" + battleSound;
                        break;
                    case 3:
                        battleSound = "battleC" + battleSound;
                        break;
                    case 4:
                        battleSound = "battleD" + battleSound;
                        break;
                    default:
                        battleSound = "battleA" + battleSound;
                        break;
                }
                DependencyService.Get<IAudioPlayerService>().Play(battleSound);
                BattleEngineView.UpdateAction(ref counter);
            }
        }

        async void OnAlertYesNoClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Game Ended", "Go to the score page!", "Yes", "No");
            if (answer)  // yes clicked
            {
                await Navigation.PushAsync(new ScorePage());
            }
            else // go to the main page
            {
                await Navigation.PopToRootAsync();
            }
        }

        async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var character = args.SelectedItem as Character;

            if (character == null)
                return;

            this.characterStatsPage = new CharacterStatsPage(new CharacterDetailViewModel(character));
            characterStatsPage.setCharacterController(characterController);

            await Navigation.PushAsync(this.characterStatsPage);

            // Manually deselect item
            CharacterListView.SelectedItem = null;
        }
    }
}


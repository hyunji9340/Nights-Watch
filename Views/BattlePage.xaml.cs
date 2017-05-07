using GroupProject_DD.Views;
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

        public BattlePage()
		{
			InitializeComponent();
            DependencyService.Get<IAudioPlayerService>().Pause();
            DependencyService.Get<IAudioPlayerService>().Play("prelude");
            BindingContext = BattleEngineView = new BattleViewModel();
			counter = 0;
            rand = new Random();
            this.characterController = new CharacterController();
        }

		async void Button_Clicked(object sender, EventArgs e)
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


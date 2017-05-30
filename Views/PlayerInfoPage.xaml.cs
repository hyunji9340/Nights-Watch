using System;
using System.Collections.Generic;
using System.Diagnostics;
using GroupProject_DD.ViewModels;

using Xamarin.Forms;

namespace GroupProject_DD
{
    public partial class PlayerInfoPage : ContentPage
    {
        private PlayerController playerController;
		// get server items at this point
		public ServerItemController serverItemController;
        public BattleEffectController battleEffectCntrl;

        public PlayerInfoPage()
        {
            InitializeComponent();
            this.playerController = new PlayerController();
			this.serverItemController = new ServerItemController();
            this.battleEffectCntrl = new BattleEffectController();

			SeverItemClicked.Clicked += async (s, e) =>
			{
				await serverItemController.GetItemsAsync();
                await battleEffectCntrl.GetItemsAsync();
				Player newPlayer = new Player();
				newPlayer.Name = playerName.Text;
				playerController.SavePlayer(newPlayer);
				await Navigation.PushAsync(new GameMainPage(newPlayer, serverItemController, battleEffectCntrl)); //serverItemController added
			};
        }
    }
}

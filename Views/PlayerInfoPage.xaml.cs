using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;

namespace GroupProject_DD
{
    public partial class PlayerInfoPage : ContentPage
    {
        private PlayerController playerController;
		// get server items at this point
		public ServerItemController serverItemController;

        public PlayerInfoPage()
        {
            InitializeComponent();
            this.playerController = new PlayerController();
			this.serverItemController = new ServerItemController();

			SeverItemClicked.Clicked += async (s, e) =>
			{
				await serverItemController.GetItemsAsync();
				Player newPlayer = new Player();
				newPlayer.Name = playerName.Text;
				playerController.SavePlayer(newPlayer);
				await Navigation.PushAsync(new GameMainPage(newPlayer, serverItemController)); //serverItemController added
			};
        }
    }
}

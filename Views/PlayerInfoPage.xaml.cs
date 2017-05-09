using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;

namespace GroupProject_DD
{
    public partial class PlayerInfoPage : ContentPage
    {
        private PlayerController playerController;

        public PlayerInfoPage()
        {
            InitializeComponent();
            this.playerController = new PlayerController();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            // create a player instance and store in db
            Player newPlayer = new Player();
            newPlayer.Name = playerName.Text;
            playerController.SavePlayer(newPlayer);
            await Navigation.PushAsync(new GameMainPage(newPlayer));
        }

    }
}

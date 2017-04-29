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
		public BattlePage()
		{
			InitializeComponent();
			BindingContext = BattleEngineView = new BattleViewModel();
			counter = 0;
            rand = new Random();
            
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
	}
}


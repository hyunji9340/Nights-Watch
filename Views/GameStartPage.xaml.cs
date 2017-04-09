using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinProject
{
	public partial class GameStartPage : ContentPage
	{
		public GameStartPage()
		{
			InitializeComponent();
		}

		// character
		async void characterClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CharactersPage());
		}

		// monster
		async void monsterClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MonsterPage());
		}

		// item
		async void itemClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ItemsPage());
		}

		// battle
		async void battleClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new BattlePage());
		}

		// score
		async void scoreClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ScorePage());
		}

		// inventory
		async void inventoryClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new InventoryPage());
		}
	}
}

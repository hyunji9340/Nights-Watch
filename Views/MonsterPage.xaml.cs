using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinProject
{
	public partial class MonsterPage : ContentPage
	{
		private List<Monster> monsters;

		public MonsterPage()
		{
			InitializeComponent();
			PopulateMonsters();
			MonsterListView.ItemsSource = monsters;
		}

		public void PopulateMonsters() 
		{
			monsters = new List<Monster>();
			Monster monster1 = new Monster();
			Monster monster2 = new Monster();
			Monster monster3 = new Monster();
			Monster monster4 = new Monster();
			Monster monster5 = new Monster();
			monster1.name = "monster1";
			monster2.name = "mosnter2";
			monster3.name = "monster3";
			monster4.name = "monster4";
			monster5.name = "monster5";
			monsters.Add(monster1);
			monsters.Add(monster2);
			monsters.Add(monster3);
			monsters.Add(monster4);
			monsters.Add(monster5);
		}


		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var monster = args.SelectedItem as Monster;

			if (monster == null)
				return;

			//await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(monster)));

			// Manually deselect item
			MonsterListView.SelectedItem = null;
		}

	}
}

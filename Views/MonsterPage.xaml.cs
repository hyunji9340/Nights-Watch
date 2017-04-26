using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;

using Xamarin.Forms;

namespace GroupProject_DD
{
	public partial class MonsterPage : ContentPage
	{
		private MonsterController monsterController;
		private MonsterDetailPage monsterDetailPage;

		public MonsterPage()
		{
			InitializeComponent();
			this.monsterController = new MonsterController();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			this.BindingContext = this.monsterController;
		}

		// This method directs a detail page for a specific item
		async void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var monster = args.SelectedItem as Monster;

			if (monster == null)
				return;

			this.monsterDetailPage = new MonsterDetailPage(new MonsterDetailViewModel(monster));
			monsterDetailPage.setMonsterController(monsterController);

			await Navigation.PushAsync(this.monsterDetailPage);

			// Manually deselect item
			MonsterListView.SelectedItem = null;
		}

		// directs to monster page
		async void AddMonsterBtnClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddMonsterPage(monsterController));
		}
	}
}

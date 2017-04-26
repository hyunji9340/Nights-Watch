using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GroupProject_DD
{
	public partial class MonsterDetailPage : ContentPage
	{
		MonsterDetailViewModel monsterViewModel;
		private MonsterController monsterController;

		public MonsterDetailPage(MonsterDetailViewModel monsterViewModel)
		{
			InitializeComponent();
			BindingContext = this.monsterViewModel = monsterViewModel;
		}

		public void setMonsterController(MonsterController monsterController)
		{
			this.monsterController = monsterController;
		}

		// need to call update page
		async void UpdateBtnClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddMonsterPage(monsterController, this.monsterViewModel.Monster));
		}

		async void DeleteBtnClicked(object sender, EventArgs e)
		{
			this.monsterController.DeleteMonster(this.monsterViewModel.Monster);
			await Navigation.PopToRootAsync();
		}
	}
}


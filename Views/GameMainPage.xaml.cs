using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GroupProject_DD
{
	public partial class GameMainPage : ContentPage
	{

		public GameMainPage()
		{
			InitializeComponent();
		}

		async void CharacterBtnClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CharacterPage());
		}

		async void MonsterBtnClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MonsterPage());
		}

		async void StartGameBtnClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new GameStartPage());
		}
	}
}

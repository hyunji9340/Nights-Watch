using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GroupProject_DD
{
	public partial class StudentsInfoPage : ContentPage
	{
		public StudentsInfoPage()
		{
			InitializeComponent();
		}

		async void playGameBtnClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PlayerInfoPage());
		}
	}
}


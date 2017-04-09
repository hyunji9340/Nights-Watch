using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinProject
{
	public partial class MyInfo : ContentPage
	{

		public MyInfo()
		{
			//Console.WriteLine("constructor MyInfo() has been called");
			InitializeComponent();
		}

		async void playGameBtnClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new GameStartPage());
		}
	}
}

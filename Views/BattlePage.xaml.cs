using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinProject.ViewModels;

namespace XamarinProject
{
	public partial class BattlePage : ContentPage
	{
        BattleViewModel BattleEngine;

		public BattlePage()
		{
			InitializeComponent();

            BindingContext = BattleEngine = new BattleViewModel();
		}

        async void step_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }
    }
}

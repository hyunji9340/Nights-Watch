using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinProject.ViewModels;

namespace XamarinProject
{
	public partial class BattlePage : ContentPage
	{
        BattleViewModel BattleEngine;
        int counter;

		public BattlePage()
		{
			InitializeComponent();

            BindingContext = BattleEngine = new BattleViewModel();

            counter = 0;
		}

        async void Button_Clicked(object sender, EventArgs e)
        {
            BattleEngine.UpdateAction(ref counter);
        }
    }
}

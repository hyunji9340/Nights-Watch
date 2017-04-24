using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinProject.ViewModels;

namespace XamarinProject
{
	public partial class BattlePage : ContentPage
	{
        BattleViewModel BattleEngineView;
        int counter;

		public BattlePage()
		{
			InitializeComponent();

            BindingContext = BattleEngineView = new BattleViewModel();

            counter = 0;
		}

        async void Button_Clicked(object sender, EventArgs e)
        {
            BattleEngineView.UpdateAction(ref counter);
        }
    }
}

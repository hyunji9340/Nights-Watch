using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;

namespace GroupProject_DD
{
	public partial class BattlePage : ContentPage
	{
		private BattleViewModel BattleEngineView;
		private int counter;

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


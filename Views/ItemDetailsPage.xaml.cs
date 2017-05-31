using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupProject_DD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
	{
		public Item Item;

		public ItemDetailPage(Item item)
		{
			InitializeComponent();
			this.Item = item;
			Debug.WriteLine("In detail page: " + Item.Name);
			Debug.WriteLine("In detial page: image url: " + Item.Image);
			BindingContext = this.Item;
		}
	}
}

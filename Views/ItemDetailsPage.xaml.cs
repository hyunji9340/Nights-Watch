using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace GroupProject_DD
{
	public partial class ItemDetailPage : ContentPage
	{
		public Item Item;

		public ItemDetailPage(Item item)
		{
			//InitializeComponent();
			this.Item = item;
			Debug.WriteLine("In detail page: " + Item.Name);
			Debug.WriteLine("In detial page: image url: " + Item.Image);
			BindingContext = this.Item;
		}
	}
}

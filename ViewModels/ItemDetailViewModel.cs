using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinProject
{
	public class ItemDetailViewModel : BaseViewModel
	{
		public Items Item { get; set; }
		public ItemDetailViewModel(Items items = null)
		{
			Title = items.description;
		}

		int quantity = 1;
		public int Quantity
		{
			get { return quantity; }
			set { SetProperty(ref quantity, value); }
		}
	}
}

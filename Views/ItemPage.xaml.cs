using GroupProject_DD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupProject_DD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemPage : ContentPage
    {
		List<Item> serverItems;
        ServerItemController serverItemController;
        
		public ItemPage(ServerItemController serverItemController)
        {
            InitializeComponent();
			serverItems = serverItemController.getServerItems();
			ItemListView.ItemsSource = serverItems;
        }
    }
}

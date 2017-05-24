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
        ObservableCollection<Item> serverItems;
        ServerItemController serverItemController;
        private Settings settings;
        public ItemPage(Settings IncomingSettings)
        {
            InitializeComponent();
            serverItems = new ObservableCollection<Item>();
            settings = IncomingSettings;
            Debug.WriteLine("after initialize component ");
            serverItemController = new ServerItemController(IncomingSettings, serverItems);
            Debug.WriteLine("after initialize component1 ");
            BindingContext = this;
            serverItemController.InitializeServerItems();
            
        }

        private void ItemListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}

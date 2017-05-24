using GroupProject_DD.Models;
using System;
using System.Collections.Generic;
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
        private Settings settings;
        public ItemPage(Settings IncomingSettings)
        {
            InitializeComponent();
            settings = IncomingSettings;
            Debug.WriteLine("after initialize component ");
            serverItemController = new ServerItemController(IncomingSettings);
            Debug.WriteLine("after initialize component1 ");

            serverItemController.InitializeServerItems();
            Debug.WriteLine("after initialize component2 ");

            this.serverItems = serverItemController.getServerItems();
            ItemListView.ItemsSource = serverItems;
        }
    }
}

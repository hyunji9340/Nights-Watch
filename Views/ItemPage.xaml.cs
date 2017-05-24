using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using GroupProject_DD.Models;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Diagnostics;

using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GroupProject_DD
{
    public partial class ItemPage : ContentPage
    {
        List<Item> serverItems;
        ServerItemController serverItemController;
        public ItemPage()
        {
            InitializeComponent();
            /*Debug.WriteLine("after initialize component ");
            serverItemController = new ServerItemController();
            Debug.WriteLine("after initialize component1 ");

            serverItemController.InitializeServerItems();
            Debug.WriteLine("after initialize component2 ");

            this.serverItems = serverItemController.getServerItems();
            ItemListView.ItemsSource = serverItems;*/
        }

    }
}

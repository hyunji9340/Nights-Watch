using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using GroupProject_DD.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GroupProject_DD
{
	public class ServerItemController
	{
		public Settings currentSetting;
		public ServerItems serverItems;
		List<Item> ServerItems;

		// default constructor
		public ServerItemController() 
		{
			this.currentSetting = new Settings();
			this.serverItems = new ServerItems();
			this.ServerItems = new List<Item>();
		}


		public ServerItemController(Settings incomingSettings, List<Item> displayItems)
		{
            displayItems = ServerItems;
            Debug.WriteLine("server item controller instantiated");
			currentSetting = incomingSettings;
		}

		public List<Item> getServerItems() 
		{
			if (ServerItems == null || ServerItems.Count == 0)
			{
				return new List<Item>();
			}
			return ServerItems;
		}


		public async Task<string> GetItemsAsync() 
		{
			bool isRandom = currentSetting.ItemsRandom;
			bool isSuperItem = currentSetting.SuperItems;
			int intVersionIsRandom = 1;
			int intVersionIsSuperItem = 1;
			if (!isRandom) { intVersionIsRandom = 0; }
			if (!isSuperItem) { intVersionIsSuperItem = 0; }

			var client = new System.Net.Http.HttpClient();
			string stringIsRandom = Convert.ToString(intVersionIsRandom);
			string stringIsSuperItem = Convert.ToString(intVersionIsSuperItem);

			var values = new Dictionary<string, string>
			{
				{ "randomItemOption", stringIsRandom },
				{ "superItemOption", stringIsSuperItem }
			};

			var content = new FormUrlEncodedContent(values);
			var response = await client.PostAsync("http://gamehackathon.azurewebsites.net/api/GetItemsList", content);
			var itemsJson = response.Content.ReadAsStringAsync().Result;
			this.serverItems = JsonConvert.DeserializeObject<ServerItems>(itemsJson);
            ServerItems = serverItems.data;
			return itemsJson;
        }
	}
}

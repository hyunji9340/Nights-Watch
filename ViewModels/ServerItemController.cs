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

namespace GroupProject_DD
{
	public class ServerItemController
	{
		Settings currentSetting;
		ServerItems serverItems;
		List<Item> ServerItems;

		public ServerItemController(Settings incomingSettings)
		{
			Debug.WriteLine("server item controller instantiated");
			currentSetting = incomingSettings;
		}

		public void InitializeServerItems() 
		{
			Debug.WriteLine("initializer server item called");
			bool isRandom = currentSetting.ItemsRandom;
			bool isSuperItem = currentSetting.SuperItems;
			int intVersionIsRandom = 1;
			int intVersionIsSuperItem = 1;
			if (!isRandom) { intVersionIsRandom = 0; }
			if (!isSuperItem) { intVersionIsSuperItem = 0; }
			GetItemsAsync(intVersionIsRandom, intVersionIsSuperItem);
			ServerItems = serverItems.data;
		}

		public List<Item> getServerItems() 
		{
			if (ServerItems == null || ServerItems.Count == 0)
			{
				return new List<Item>();
			}
			return ServerItems;
		}


		public async void GetItemsAsync(int isRandom, int isSuperItem)
		{
			Debug.WriteLine("Get Item Asyn called");
			var client = new System.Net.Http.HttpClient();
			Debug.WriteLine("IsRandom: " + isRandom);
			Debug.WriteLine("is super Item: " + isSuperItem);
			string stringIsRandom = Convert.ToString(isRandom);
			string stringIsSuperItem = Convert.ToString(isSuperItem);
			Debug.WriteLine("IsRandom: " + isRandom);
			Debug.WriteLine("is super Item: " + isSuperItem);

			var values = new Dictionary<string, string>
			{
				{ "", "" },
			};
			Debug.WriteLine("values created ");

			var content = new FormUrlEncodedContent(values);
			Debug.WriteLine("converted to url form");
			var response = await client.PostAsync("http://gamehackathon.azurewebsites.net/api/GetItemsList", content);
			Debug.WriteLine("after response");
			var itemsJson = response.Content.ReadAsStringAsync().Result;
			Debug.WriteLine("after itemJson convert");
			//ItemAPI apiItems = JsonConvert.DeserializeObject<ItemAPI>(itemJson);
			this.serverItems = JsonConvert.DeserializeObject<ServerItems>(itemsJson);
			Debug.WriteLine("after conversion deserialization");

			return;
		}
	}
}

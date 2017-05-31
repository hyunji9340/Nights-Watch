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

namespace GroupProject_DD.ViewModels
{
    public class BattleEffectController
    {

        public Settings currentSetting;
        public serverEvents ServerEvents;
        public List<BattleEffects> Effects;

        public BattleEffectController()
        {
            this.currentSetting = new Settings();
            this.ServerEvents = new serverEvents();
            this.Effects = new List<BattleEffects>();
        }

        public List<BattleEffects> getServerEffects()
        {
            if (Effects == null || Effects.Count == 0)
            {
                return new List<BattleEffects>();
            }
            return Effects;
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
            var response = await client.PostAsync("http://gamehackathon.azurewebsites.net/api/GetBattleEffects", content);
            var itemsJson = response.Content.ReadAsStringAsync().Result;
            this.ServerEvents = JsonConvert.DeserializeObject<serverEvents>(itemsJson);
            Effects = ServerEvents.data;
            return itemsJson;
        }


    }

}

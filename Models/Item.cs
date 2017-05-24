using GroupProject_DD.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;


namespace GroupProject_DD
{
    

	public class Item 
	{
        //tier is ranged from 1 - 10 based on how dominate an item can be
         Random rand;
        /*public Dictionary<string, int> Attributes;
        public string bodyassignment;
		// fields 
		public String name { get; set; }
		public String description { get; set; }
		public int tier { get; set; }
		public String type { get; set; }
		public int slot { get; set; } */

        [JsonProperty("Image")]
        public string Image { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Tier")]
        public int Tier { get; set; }
        [JsonProperty("BodyPart")]
        public string BodyPart { get; set; }
        [JsonProperty("AttribMod")]
        public string AttribMod { get; set; }

        [JsonProperty("Usage")]
        public int Usage { get; set; }


        //copy constructor
        public Item(Item item)
		{
            this.Image = item.Image;
            this.Name = item.Name;
            this.Description = item.Description;
            this.Tier = item.Tier;
            this.BodyPart = item.BodyPart;
            this.AttribMod = item.AttribMod;
            this.Usage = item.Usage;
            rand = new Random();

        }

        public Item()
        {
            this.Image = "";
            this.Name = "Empty";
            this.Description = "";
            this.Tier = 0;
            this.BodyPart = "None";
            this.AttribMod = "";
            this.Usage = 0;
            rand = new Random();

        }

        public void setAttributes(int tier)
        {
            /*
            int dexterity = 0;
            int health = 0;
            int strength = 0;
            int defense = 0;
            int agility = 0;
            */
            /*comment template: Primary Attribute bonus, secondary attribute bonus
            primary bonus:
                lowerbound = tier level
                upperbound = tier level x2
           secondary bonus:
                lowerbound = tier - tier/2
                upperbound = tier level
            rest of attributes:
                lowerbound = 0
                upperbound = tier/2
            */
            /*if (bodyassignment == Bodypart.Head)//Dexterity, Defense
            {
                dexterity = rand.Next(tier, tier*2);
                health = rand.Next(0, tier / 2);
                strength = rand.Next(0, tier / 2);
                defense = rand.Next(tier - (tier / 2), tier);
                agility = rand.Next(0, tier / 2);
            }
            else if (bodyassignment == Bodypart.AttkArm)//Strength, Agility
            {
                dexterity = rand.Next(0, tier / 2);
                health = rand.Next(0, tier / 2);
                strength = rand.Next(tier, tier * 2);
                defense = rand.Next(0, tier / 2);
                agility = rand.Next(tier - (tier / 2), tier);
            }
            else if (bodyassignment == Bodypart.DefArm)//Defense, Health
            {
                dexterity = rand.Next(0, tier / 2);
                health = rand.Next(tier - (tier / 2), tier);
                strength = rand.Next(0, tier / 2);
                defense = rand.Next(tier, tier * 2);
                agility = rand.Next(0, tier / 2);
            }
            else if (bodyassignment == Bodypart.Torso)//Health, Defense
            {
                dexterity = rand.Next(0, tier / 2);
                health = rand.Next(tier, tier * 2);
                strength = rand.Next(0, tier / 2);
                defense = rand.Next(tier - (tier / 2), tier);
                agility = rand.Next(0, tier / 2);
            }
            else if (bodyassignment == Bodypart.Feet)//Agility, Dexterity
            {
                dexterity = rand.Next(tier - (tier / 2), tier);
                health = rand.Next(0, tier / 2);
                strength = rand.Next(0, tier / 2);
                defense = rand.Next(0, tier / 2);
                agility = rand.Next(tier, tier * 2);
            }*/
            /*Attributes.Add("str", strength);
            Attributes.Add("dex", dexterity);
            Attributes.Add("def", defense);
            Attributes.Add("HP", health);
            Attributes.Add("agl", agility);*/
        }

        public Item(String image, String name, String description, int Tier, string body, string attribute, int usage)
		{
            this.Image = image;
			this.Name = name;
			this.Description = description;
			this.Tier = Tier;
			this.BodyPart = body;
            this.AttribMod = attribute;
            this.Usage = usage;
            rand = new Random();
        }
	}
}
using GroupProject_DD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GroupProject_DD
{
    

	public class Item 
	{
        //tier is ranged from 1 - 10 based on how dominate an item can be
        Random rand;
        public Dictionary<string, int> Attribute;
        public string bodyassignment;
		// fields 
		public String name { get; set; }
		public String description { get; set; }
		public int tier { get; set; }
		public String type { get; set; }
		public int slot { get; set; }

        // default constructor that doesn't take any arguments
        //used to store temporary empty inventory
        public Item()
        { }


		//copy constructor
		public Item(Item item)
		{
			name = item.name;
			description = item.description;
			tier = item.tier;
            bodyassignment = item.bodyassignment;
			slot = item.slot;
            Attribute = new Dictionary<string, int>(item.Attribute);
            rand = new Random();
        }

        public void setAttributes(int tier)
        {
            int dexterity = 0;
            int health = 0;
            int strength = 0;
            int defense = 0;
            int agility = 0;
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
            if (bodyassignment == Bodypart.Head)//Dexterity, Defense
            {
                dexterity = rand.Next(tier, tier*2);
                health = rand.Next(0, tier / 2);
                strength = rand.Next(0, tier / 2);
                defense = rand.Next(tier - tier / 2, tier);
                agility = rand.Next(0, tier / 2);
            }
            else if (bodyassignment == Bodypart.AttkArm)//Strength, Agility
            {
                dexterity = rand.Next(0, tier / 2);
                health = rand.Next(0, tier / 2);
                strength = rand.Next(tier, tier * 2);
                defense = rand.Next(0, tier / 2);
                agility = rand.Next(tier - tier / 2, tier);
            }
            else if (bodyassignment == Bodypart.DefArm)//Defense, Health
            {
                dexterity = rand.Next(0, tier / 2);
                health = rand.Next(tier - tier / 2, tier);
                strength = rand.Next(0, tier / 2);
                defense = rand.Next(tier, tier * 2);
                agility = rand.Next(0, tier / 2);
            }
            else if (bodyassignment == Bodypart.Torso)//Health, Defense
            {
                dexterity = rand.Next(0, tier / 2);
                health = rand.Next(tier, tier * 2);
                strength = rand.Next(0, tier / 2);
                defense = rand.Next(tier - tier / 2, tier);
                agility = rand.Next(0, tier / 2);
            }
            else if (bodyassignment == Bodypart.Feet)//Agility, Dexterity
            {
                dexterity = rand.Next(tier - tier / 2, tier);
                health = rand.Next(0, tier / 2);
                strength = rand.Next(0, tier / 2);
                defense = rand.Next(0, tier / 2);
                agility = rand.Next(tier, tier * 2);
            }
            Attribute.Add("str", strength);
            Attribute.Add("dex", dexterity);
            Attribute.Add("def", defense);
            Attribute.Add("HP", health);
            Attribute.Add("Agl", agility);
        }

        /*
         * type can only be one of the following, controlled by Bodypart class
                "HEAD"
                "ATTK_ARM"
                "DEF_ARM"
                "TORSO"
                "FEET"
        */
        public Item(String name, String description, int Tier, string body)
		{
			this.name = name;
			this.description = description;
			this.tier = Tier;
			this.bodyassignment = body;
            Attribute = new Dictionary<string, int>();
            rand = new Random();
        }
	}
}
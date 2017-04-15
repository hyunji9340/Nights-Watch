using System;
namespace XamarinProject
{
	public class Character
	{
		//fields
		public string Name { get; set; }
		public int Level { get; set; }
		public int maxHealth { get; set; }
		public int curHealth { get; set; }
		public int Experience { get; set; }
		public int Strength { get; set; }
		public int Dexterity { get; set; }
		public int Agility { get; set; }
		public int Speed { get; set; }
		public Item[] Inventory { get; set; }

		// constructor
		public Character()
		{
			Name = "";
			Level = 1;
			maxHealth = 20;
			curHealth = 20;
			Experience = 0;
			Strength = 1;
			Dexterity = 1;
			Agility = 1;
			Speed = 1;
		}

		bool isDead() { return (curHealth <= 0); }

		void levelUp()
		{
			maxHealth += 5;
			curHealth = maxHealth;
			Strength++;
			Dexterity++;
			Agility++;
			Speed++;
		}


		void addExperience(int gain)
		{
			Experience += gain;
			if (Experience > 10) //10 is used in this case, will be replaced by global xp thresholds
			{
				levelUp();
				Experience -= 10;
			}
		}


		void addItem(Item item)
		{
			item = new Item();
			item.type = "Sword/RightHand";
			item.name = "The Shiny Blade of Dragon Slaying";
			item.description = "This sword slays dragons yo.";

			Inventory[0] = item;
		}
	}
}

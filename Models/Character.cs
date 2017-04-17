using System;
namespace XamarinProject
{
	public class Character
	{
		//fields
		public string Name { get; set; }
		public int Level { get; set; }
		public int Health { get; set; }
		public int curHealth { get; set; }
		public int Experience { get; set; }
		public int Strength { get; set; }
		public int Dexterity { get; set; }
		public int Agility { get; set; }
		public int Speed { get; set; }
		public Item[] Inventory { get; set; }
        public int monstersKilled { get; set; }
		// constructor
		public Character()
		{
			Name = "";
			Level = 1;
			Health = 20;
			curHealth = 20;
			Experience = 0;
			Strength = 1;
			Dexterity = 1;
			Agility = 1;
			Speed = 1;
            Inventory = new Item[7];
		}

		public bool isDead() { return (curHealth <= 0); }

		public void levelUp()
		{
			Health += 5;
			curHealth = Health;
			Strength++;
			Dexterity++;
			Agility++;
			Speed++;
		}

        public void takeDamage(int damage)
        {
            //Health -= damage;
        }

        public void addExperience(int gain)
		{
			Experience += gain;
			if (Experience > 10) //10 is used in this case, will be replaced by global xp thresholds
			{
				//levelUp();
				//Experience -= 10;
			}
		}


		public void addItem(Item item)
		{
            if (Inventory[item.slot].description == "")
			    Inventory[item.slot] = item;
            else
            {
                if (Inventory[item.slot].rating <= item.rating)
                    Inventory[item.slot] = item;
            }
		}
	}
}

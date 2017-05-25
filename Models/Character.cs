using System;
using SQLite;
using System.ComponentModel;
using System.Collections.Generic;
using GroupProject_DD.Models;
using System.Diagnostics;

namespace GroupProject_DD
{
	[Table("Characters")]
	public class Character : INotifyPropertyChanged, ICreature
	{
        public Dictionary<string, Item> Inventory;
        //public Dictionary<string, Item> Inventory
        //{
        //    get { return inventory; }
        //    set
        //    {
        //        inventory = value;
        //        OnPropertyChanged(nameof(Inventory));
        //    }
        //}

        private int _id;
		[PrimaryKey, AutoIncrement]
		public int Id
		{
			get { return _id; }
			set
			{
				this._id = value;
				OnPropertyChanged(nameof(Id));

			}
		}

        public string Image { get; set; }

		private string _Name;
		[NotNull]
		public string Name
		{
			get { return _Name; }
			set
			{
				this._Name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		private int _Level;
		public int Level
		{
			get { return _Level; }
			set
			{
				_Level = value;
				OnPropertyChanged(nameof(Level));
			}
		}

		private int _Health;
		public int Health
		{
			get { return _Health; }
			set
			{
				_Health = value;
				OnPropertyChanged(nameof(Health));
			}
		}

		private int _curHealth;
		public int curHealth
		{
			get {
                Debug.WriteLine("Current health: {0}", _curHealth);
                return _curHealth; }
			set
			{
				_curHealth = value;
				OnPropertyChanged(nameof(curHealth));
			}
		}

		private int _Experience;
		public int Experience
		{
			get { return _Experience; }
			set
			{
				_Experience = value;
				OnPropertyChanged(nameof(Experience));
			}
		}

		private int _Strength;
		public int Strength
		{
			get { return _Strength; }
			set
			{
				_Strength = value;
				OnPropertyChanged(nameof(Strength));
			}
		}

		private int _Dexterity;
		public int Dexterity
		{
			get { return _Dexterity; }
			set
			{
				_Dexterity = value;
				OnPropertyChanged(nameof(Dexterity));
			}
		}

		private int _Defense;
		public int Defense
		{
			get { return _Defense; }
			set
			{
				_Defense = value;
				OnPropertyChanged(nameof(Defense));
			}
		}

		private int _Speed;
		public int Speed
		{
			get { return _Speed; }
			set
			{
				_Speed = value;
				OnPropertyChanged(nameof(Speed));
			}
		}

		private int _monstersKilled;
		public int monstersKilled
		{
			get { return _monstersKilled; }
			set
			{
				_monstersKilled = value;
				OnPropertyChanged(nameof(monstersKilled));
			}
		}

        private float healthstatus;
        public float HealthStatus
        {
            set
            {
                healthstatus = value;
                OnPropertyChanged(nameof(HealthStatus));
            }
            get
            {
                Debug.WriteLine("Health Status: {0}", healthstatus);
                return healthstatus;
            }
        }

        private int agility;
        public int Agility
        {
            set
            {
                agility = value;
                OnPropertyChanged(nameof(Agility));
            }
            get
            {
                return agility;
            }
        }

        public List<String> CharacterImages = new List<String>()
        {
            "https://s-media-cache-ak0.pinimg.com/736x/4c/be/8e/4cbe8e4e51f35238f46eb740aa9eabdf--dungeons-and-dragons-characters-fantasy-characters.jpg",
            "https://s-media-cache-ak0.pinimg.com/736x/f2/9a/37/f29a3770434259a1015abbdc8ccac6a4.jpg",
            "https://s-media-cache-ak0.pinimg.com/736x/a8/0a/33/a80a3360d8ad2d0bf6789689ee4fd111.jpg",
            "http://img01.deviantart.net/342a/i/2014/148/f/9/dungeons_and_dragons_character_commission_by_leahmsmith-d7k2ywh.png",
            "http://dungeonsmaster.com/wp-content/uploads/2010/09/53.jpg"
        };

        //constructor
        public Character()
        {
            Random rand = new Random();
            
            Image = CharacterImages[rand.Next(0,4)];
            Name = "";
            Level = 1;
            Health = 20;
            curHealth = Health;
            Experience = 0;
            Strength = 1;
            Dexterity = 1;
            Defense = 1;
            Speed = 3;
            monstersKilled = 0;
            HealthStatus = 1;
            Inventory = new Dictionary<string, Item>()
            {
                {Bodypart.Head, new Item() },
                {Bodypart.AttkArm, new Item()},
                {Bodypart.DefArm, new Item()},
                {Bodypart.Torso, new Item()},
                {Bodypart.Feet, new Item()},
                {Bodypart.MagicAll, new Item()},
                {Bodypart.MagicDirect, new Item()}
            };
        }

        public Character(String name)
        {
            Random rand = new Random();

            Image = CharacterImages[rand.Next(0, 4)];
            Name = name;
            Level = 1;
            Health = 20;
            curHealth = Health;
            Experience = 0;
            Strength = 1;
            Dexterity = 1;
            Defense = 1;
            Speed = 3;
            monstersKilled = 0;
            HealthStatus = 1;

            Inventory = new Dictionary<string, Item>()
            {
                {Bodypart.Head, new Item()},
                {Bodypart.AttkArm, new Item()},
                {Bodypart.DefArm, new Item()},
                {Bodypart.Torso, new Item()},
                {Bodypart.Feet, new Item()},
                {Bodypart.MagicAll, new Item()},
                {Bodypart.MagicDirect, new Item()}
            };
            Item item = Inventory[Bodypart.AttkArm];
        }

        public void ResetStatus()
        {
            curHealth = Health;
            HealthStatus = 1;
        }

        public int Attack()
  		{
			int attack = ((2*Level)/5+2)*Strength;
  			return attack;
  		}

		public bool isDead()
		{
			return (curHealth <= 0);
		}

		public void levelUp()
		{
			Level++;
			Health += 5;
			curHealth = Health;
			Strength++;
			Dexterity++;
			Defense++;
			Speed++;
            HealthStatus = 1;
        }

  		public int takeDamage(int damage)
		{
            int dmg = (damage / Defense) + 2;
            curHealth -= dmg;
            HealthStatus = (float)curHealth / (float)Health;
            return dmg;
		}

		public bool addExperience(int gain)
		{
			Experience += gain;
			if (Experience > (10*Level)) //10 is used in this case, will be replaced by global xp thresholds
			{
				levelUp();
				Experience = 0;
                return true;
			}
            return false;
		}

        public bool evaluateNewItem(Item item)
        {
            /*locate item slot in inventory*/
            Item temp_Item = Inventory[item.BodyPart];

            /*slot is empty, fill with new item found*/
            if (temp_Item.Name == "Empty")
            {
                Debug.WriteLine("tempItem {0}", temp_Item.BodyPart);
                temp_Item.BodyPart = item.BodyPart;
                Debug.WriteLine("tempItem new {0}", temp_Item.BodyPart);
                discardItem(temp_Item);
                equipItem(item);
                return true;
            }
            else/*slot is already filled, evaluate better item (new or old)*/
            {
                /*new item is better, discard old item, else ignore new item*/
                if (temp_Item.Tier <= item.Tier)
                {
                    discardItem(temp_Item);
                    equipItem(item);
                    verifyHealth();
                    return true;
                }
            }
            return false;
        }

        public void equipItem(Item item)
        {
            Debug.WriteLine("Equipped: {0}", item.Name);
            Debug.WriteLine("Equipped: {0}", item.BodyPart);
            Inventory.Add(item.BodyPart, item);
            addAttributes(item);
        }

        public void discardItem(Item item)
        {
            removeAttributes(item);
            Inventory.Remove(item.BodyPart);
            /*
            Could be scenario where max health is now lower than current health
            eavlutate whether to reset current health value; 
            */
        }

        public void verifyHealth()
        {
            if (Health < curHealth)
                curHealth = Health;
        }

        
        public void removeAttributes(Item item)
        {
           
                if (item.AttribMod == "STRENGTH")
                {
                    Strength -= item.Tier;
                }
                else if (item.AttribMod == "DEXTERITY")
                {
                    Dexterity -= item.Tier;
                }
                else if (item.AttribMod == "DEFENSE")
                {
                    Defense -= item.Tier;
                }
                else if (item.AttribMod == "HP")
                {
                    Health -= item.Tier;
                }
                else if (item.AttribMod == "agl")
                {
                    Agility -= item.Tier;
                }
            
        }

        public void addAttributes(Item item)
        {

            if (item.AttribMod == "STRENGTH")
            {
                Strength += item.Tier;
            }
            else if (item.AttribMod == "DEXTERITY")
            {
                Dexterity += item.Tier;
            }
            else if (item.AttribMod == "DEFENSE")
            {
                Defense += item.Tier;
            }
            else if (item.AttribMod == "HP")
            {
                Health += item.Tier;
            }
            else if (item.AttribMod == "agl")
            {
                Agility += item.Tier;
            }

        }
        

        public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

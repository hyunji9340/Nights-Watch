using System;
using SQLite;
using System.ComponentModel;
using System.Collections.Generic;
using GroupProject_DD.Models;

namespace GroupProject_DD
{
	[Table("Characters")]
	public class Character : INotifyPropertyChanged, ICreature
	{

        public Dictionary<string, Item> Inventory;

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
			get { return _curHealth; }
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

        //constructor
        public Character()
        {
            Name = "";
            Level = 1;
            Health = 20;
            curHealth = Health;
            Experience = 0;
            Strength = 1;
            Dexterity = 1;
            Defense = 1;
            Speed = 1;
            monstersKilled = 0;
            HealthStatus = 1;
            Inventory = new Dictionary<string, Item>()
            {
                {Bodypart.Head, new Item("Empty", "", 0, Bodypart.Head)},
                {Bodypart.AttkArm, new Item("Empty", "", 0, Bodypart.AttkArm)},
                {Bodypart.DefArm, new Item("Empty", "", 0, Bodypart.DefArm)},
                {Bodypart.Torso, new Item("Empty", "", 0, Bodypart.Torso)},
                {Bodypart.Feet, new Item("Empty", "", 0, Bodypart.Feet)}
            };
        }

        public Character(String name)
        {
            Name = name;
            Level = 1;
            Health = 20;
            curHealth = Health;
            Experience = 0;
            Strength = 1;
            Dexterity = 1;
            Defense = 1;
            Speed = 1;
            monstersKilled = 0;
            HealthStatus = 1;
            Inventory = new Dictionary<string, Item>()
            {
                {Bodypart.Head, new Item("Empty", "", 0, Bodypart.Head)},
                {Bodypart.AttkArm, new Item("Empty", "", 0, Bodypart.Head)},
                {Bodypart.DefArm, new Item("Empty", "", 0, Bodypart.Head)},
                {Bodypart.Torso, new Item("Empty", "", 0, Bodypart.Head)},
                {Bodypart.Feet, new Item("Empty", "", 0, Bodypart.Head)}
            };
        }

        public void ResetStatus()
        {
            curHealth = Health;
            HealthStatus = 1;
        }

        public int Attack()
  		{
			int attack = Strength;
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
			curHealth -= damage;
            HealthStatus = (float)curHealth / (float)Health;
            return damage;
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
            Item temp_Item;
            Inventory.TryGetValue(item.bodyassignment, out temp_Item);

            /*slot is empty, fill with new item found*/
            if (temp_Item.name == "Empty")
            {
                discardItem(temp_Item);
                equipItem(item);
                return true;
            }
            else/*slot is already filled, evaluate better item (new or old)*/
            {
                /*new item is better, discard old item, else ignore new item*/
                if (temp_Item.tier <= item.tier)
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
            Inventory.Add(item.bodyassignment, item);
            addAttributes(item);
        }

        public void discardItem(Item item)
        {
            removeAttributes(item);
            Inventory.Remove(item.bodyassignment);
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
            foreach (KeyValuePair<string, int> attribute in item.Attributes)
            {
                if (attribute.Key == "str")
                {
                    Strength -= attribute.Value;
                }
                else if (attribute.Key == "dex")
                {
                    Dexterity -= attribute.Value;
                }
                else if (attribute.Key == "def")
                {
                    Defense -= attribute.Value;
                }
                else if (attribute.Key == "HP")
                {
                    Health -= attribute.Value;
                }
                else if (attribute.Key == "agl")
                {
                    Agility -= attribute.Value;
                }
            }
        }

        public void addAttributes(Item item)
        {
            foreach (KeyValuePair<string, int> attribute in item.Attributes)
            {
                if (attribute.Key == "str")
                {
                    Strength += attribute.Value;
                }
                else if (attribute.Key == "dex")
                {
                    Dexterity += attribute.Value;
                }
                else if (attribute.Key == "def")
                {
                    Defense += attribute.Value;
                }
                else if (attribute.Key == "HP")
                {
                    Health += attribute.Value;
                }
                else if (attribute.Key == "agl")
                {
                    Agility += attribute.Value;
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

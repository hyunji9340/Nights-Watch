using System;
using SQLite;
using System.ComponentModel;

namespace GroupProject_DD
{
	[Table("Characters")]
	public class Character : INotifyPropertyChanged
	{
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

		/*
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
			Defense = 1;
			Speed = 1;
			monstersKilled = 0;
			torsoItem = new Item();
			headItem = new Item();
			rightArmItem = new Item();
			leftArmItem = new Item();
			legsItem = new Item();
		}

		public Character(String name)
		{
			Name = name;
			Level = 1;
			Health = 20;
			curHealth = 20;
			Experience = 0;
			Strength = 1;
			Dexterity = 1;
			Defense = 1;
			Speed = 1;
			monstersKilled = 0;
			torsoItem = new Item();
			headItem = new Item();
			rightArmItem = new Item();
			leftArmItem = new Item();
			legsItem = new Item();
		}
		
		*/

        public int Attack()
  		{
			int attack = Strength;
			//if  (Inventory[1] != null)
  				//attack += Inventory[1].rating;
  			return attack;
  		}

		public bool isDead()
		{
			return (Health <= 0);
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
		}

  		public void takeDamage(int damage)
		{
			Health -= damage;
		}

		public void addExperience(int gain)
		{
			Experience += gain;
			if (Experience > 10) //10 is used in this case, will be replaced by global xp thresholds
			{
				levelUp();
				Experience -= 10;
			}
		}
		/*
		public void addItem(Item item)
		{
			if (Inventory[item.slot] == null)
			  	Inventory[item.slot] = item;
  			else
			{
				if (Inventory[item.slot].rating <= item.rating)
  					Inventory[item.slot] = item;
			}

		}
		*/
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

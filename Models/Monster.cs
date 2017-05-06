using System;
using SQLite;
using System.ComponentModel;
using GroupProject_DD.Models;

namespace GroupProject_DD
{
	[Table("Monsters")]
	public class Monster : INotifyPropertyChanged, ICreature
	{
		public Monster()
		{
            curHealth = 15;
		}

        public Monster(Monster monster)
        {
            Name = monster.Name;
        }


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
		public int curHealth
        {
			get { return _Health; }
			set
			{
				_Health = value;
				OnPropertyChanged(nameof(curHealth));
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

		private int _Agility;
		public int Agility
		{
			get { return _Agility; }
			set
			{
				_Agility = value;
				OnPropertyChanged(nameof(Agility));
			}
		}

        private Item item { get; set; }

        public void setMonsterLevel(int level)
		{
			Level = level;
            curHealth = 5 * level;
            Experience = 5 * level;
			Strength = level;
			Dexterity = level;
			Agility = level;
			Speed = level;
		}

		public bool isDead()
		{
			return (curHealth <= 0);
		}

		public int takeDamage(int damage)
		{
            curHealth -= damage;
            return damage;
		}

        public void addItem(Item item)
        {
            this.item = item;
        }

        public Item discardItem()
        {
            return item;
        }

        public bool hasItem()
        {
            if (item == null)
                return false;
            else
                return true;
        }

        public int Attack()
		{
			return Strength + Level;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

using System;
using SQLite;
using System.ComponentModel;
using GroupProject_DD.Models;
using System.Collections.Generic;

namespace GroupProject_DD
{
	[Table("Monsters")]
	public class Monster : INotifyPropertyChanged, ICreature
	{
        public List<String> MonsterImages = new List<String>()
        {
            "https://img.wonderhowto.com/img/64/06/63581561216775/0/become-game-thrones-white-walker-halloween.w1456.jpg",
            "https://s-media-cache-ak0.pinimg.com/736x/b8/f4/68/b8f468548840f7f9fa3f81df253ef2a0.jpg",
            "https://s-media-cache-ak0.pinimg.com/736x/d6/e0/15/d6e015b57fed8899b388f44784992f56.jpg",
            "https://generalmills.promo.eprize.com/monsters/public/images/boo_placeholder.png",
            "https://vignette2.wikia.nocookie.net/muppet/images/0/08/CookieMonsterWaving.jpg/revision/latest?cb=20120128192952"
        };
        public Monster()
		{
            Random rand = new Random();

            curHealth = 15;
            Image = MonsterImages[rand.Next(0, 4)];
            setMonsterLevel(1);
		}

        public string Image { get; set; }

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
            Experience = 10 * level;
			Strength = 2*level;
			Dexterity = 2*level;
			Agility = level;
			Speed = level;
            Defense = 4*level;
		}

		public bool isDead()
		{
			return (curHealth <= 0);
		}

		public int takeDamage(int damage)
		{
            int dmg = (damage / Defense) + 2;
            curHealth -= dmg;
            return dmg;
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

        public int generateAttackDmg()
		{
			//return ((2 * Level) / 5 + 2) * Strength;
			return ((2 * Level) / 5 + 2);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

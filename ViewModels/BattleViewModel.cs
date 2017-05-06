using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using GroupProject_DD.Models;

namespace GroupProject_DD
{
	public class BattleViewModel : INotifyPropertyChanged
	{
		/******************Developer Fields**************/
		Character char1;
		Character char2;
		Character char3;
		Character char4;

		/*****************Actual Fields**********************/
		List<Item> item_dictionary;
		List<ICreature> CharacterList;
		List<Monster> monster_dictionary;
        List<ICreature> activeMonsterList;
		public ObservableCollection<String> actions;
        public ObservableCollection<Character> CharacterReadoutList;
        public string action;
		Engine BattleEngine;
		bool steplock;

		/*****************Controllers**********************/
		CharacterController characterController = new CharacterController();
		MonsterController monsterController = new MonsterController();

		public BattleViewModel()
		{
            /**********For Developer Mode (*Fixed Character and Item List*)**********/
            devStartup();
            /**********************************************************************/
            action = "Game starting...";
            actions = new ObservableCollection<string>();
            steplock = false;
            BattleEngine = new Engine(CharacterList, item_dictionary, monster_dictionary);
            startGameLoop(true);
        }

		public void startGameLoop(bool manual_mode)
		{
			Enque(BattleEngine.IncrementDungeonLevel());
			Enque(BattleEngine.generateMonsterList(1));
			getMonsterList();

		}

		public void getMonsterList()
		{
            activeMonsterList = BattleEngine.currentMonsterList();
			foreach (Monster monster in activeMonsterList)
			{
				Enque("Level " + monster.Level + " " + monster.Name);
			}
			Enque("");
		}


		public void devStartup()
		{
			// access to db and fetch data
			IEnumerable < ICreature > allCharactersInDB = characterController.GetAllItems();
			// convert ienumerable to array
			this.CharacterList = allCharactersInDB.ToList();
            CharacterReadoutList = new ObservableCollection<Character>();
            foreach (Character hero in CharacterList)
            {
                CharacterReadoutList.Add(hero);
            }
            IEnumerable<Monster> allMonstersInDB = monsterController.GetAllMonsters();
			this.monster_dictionary = allMonstersInDB.ToList();

			item_dictionary = new List<Item>()
			{
				new Item("Sword", "Typical Sword", 2, Bodypart.AttkArm),
				new Item("Leather Armor", "Torso Protection", 1, Bodypart.Torso),
				new Item("Stupid Helmet", "It's dumb", 1, Bodypart.Head),
				new Item("Mythril Sheild", "Heavy", 5, Bodypart.DefArm),
				new Item("Silver Sword", "Elegant Sword", 4, Bodypart.AttkArm),
				new Item("Silver Helmet", "Elegant Helmet", 4, Bodypart.Head),
				new Item("Bronze Armor", "Rustic", 4, Bodypart.Torso),
				new Item("Chainmail", "It's Bulletproof", 4, Bodypart.Torso),
				new Item("Winged Boots", "It's so light", 7, Bodypart.Feet)
			};

		}

		public void Enque(string log)
		{
			if (actions.Count >= 11)
				actions.RemoveAt(actions.Count - 1);
			actions.Insert(0, log);
		}

		public ObservableCollection<string> Actions
		{
			get
			{
				return actions;
			}
			set
			{
				actions = value;
				OnPropertyChanged("Actions");
			}
		}

        public string MonsterCount
        {
            get
            {
                OnPropertyChanged("MonsterCount");
                return "Monsters remaining: " + BattleEngine.monsterList.Count;
            }
        }

        public ObservableCollection<Character> CharacterReadout
        {
            get
            {
                OnPropertyChanged("CharacterReadout");
                return CharacterReadoutList;
            }
        }

        public void UpdateAction(ref int counter)
        {
            if (!BattleEngine.areAnyMonstersAlive())//all monsters are dead
            {
                Enque(BattleEngine.IncrementDungeonLevel());
                BattleEngine.PlayerStatusReset();
                BattleEngine.generateMonsterList(BattleEngine.dungeonLevel);
                //sort queues based on speed
                BattleEngine.sortLists();
                Enque("");
                Enque("");
            }
            if (BattleEngine.areAnyCharactersAlive())//characters still alive
            {
                List<string> actionList;
                if (BattleEngine.CharacterIsFaster())//character first
                {
                    EnqueActions(BattleEngine.Volley(true));
                }
                else//monster first
                {
                    EnqueActions(BattleEngine.Volley(false));
                }
            }
            else
                Enque("All characters are dead....");
            OnPropertyChanged("Actions");
        }

        public void EnqueActions(List<string> actions)
        {
            foreach (string _action in actions)
            {
                Enque(_action);
            }
        }

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var changed = PropertyChanged;
			if (changed != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

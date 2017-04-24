using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using XamarinProject.Models;

namespace XamarinProject.ViewModels
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
        List<Character> CharacterList;
        List<Monster> monster_dictionary;
        public ObservableRangeCollection<String> actions;
        public string action;
        Engine BattleEngine;
        bool steplock;

        public BattleViewModel()
        {
            /**********For Developer Mode (*Fixed Character and Item List*)**********/
            devStartup();
            /**********************************************************************/
            action = "Game starting...";
            actions = new ObservableRangeCollection<string>();
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
            List<Monster> monsterlist = BattleEngine.currentMonsterList();
            foreach (Monster monster in monsterlist)
            {
                Enque("Level " + monster.Rating + " " + monster.Name);
            }
            Enque("");
        }
        public void devStartup()
        {
            CharacterList = new List<Character>()
            {
                new Character("Greg the Chancellor"),
                new Character("Sally the Destroyer"),
                new Character("Spencer the Barbarian"),
                new Character("Andrew the Jester")
            };

            item_dictionary = new List<Item>()
            {
                new Item("Sword", "Typical Sword", 1, "Strength", 1),
                new Item("Leather Armor", "Torso Protection", 2, "Strength", 2),
                new Item("Stupid Helmet", "It's dumb", 1, "Defense", 0),
                new Item("Mythril Pants", "Heavy", 5, "Speed", 3),
                new Item("Silver Sword", "Elegant Sword", 4, "Strength", 1),
                new Item("Silver Helmet", "Elegant Helmet", 4, "Defense", 0),
                new Item("Bronze Armor", "Rustic", 3, "Defense", 2),
                new Item("Chainmail", "It's Bulletproof", 3, "Defense", 2),
                new Item("Feather Shorts", "It's so light", 7, "Speed", 3)
            };
            monster_dictionary = new List<Monster>()
            {
                new Monster("Goblin"),
                new Monster("Skeleton"),
                new Monster("Troll"),
                new Monster("White-Walker"),
                new Monster("Wildling")
            };
        }

        public void Enque(string log)
        {
            if (actions.Count >= 11)
                actions.RemoveAt(actions.Count - 1);
            actions.Insert(0, log);
        }

        public ObservableRangeCollection<string> Actions
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


        public void UpdateAction(ref int counter)
        {
            if (!BattleEngine.evaluateMonsterList())
            {
                BattleEngine.IncrementDungeonLevel();
                BattleEngine.generateMonsterList(BattleEngine.dungeonLevel);
                Enque("");
                Enque("");
            }
            if (BattleEngine.evaluateCharacterList())
            {
                List<string> actionList = BattleEngine.Volley();
                foreach (string _action in actionList)
                {
                    Enque(_action);
                }
            }
            else
                Enque("All characters are dead....");
            OnPropertyChanged("Actions");
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

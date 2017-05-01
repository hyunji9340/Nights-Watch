using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Vibrate;
using GroupProject_DD.Models;

namespace GroupProject_DD
{
	public class Engine
	{
		/***********************resource declarations***************************/
		public List<Character> characterList;
		public List<Monster> monsterList;
		public List<Character> characterDeadList;
		public List<Item> item_dictionary;
		public List<Monster> monster_dictionary;
		public int dungeonLevel;
		Random rand;

		public Engine(List<Character> charList, List<Item> itemDictionary, List<Monster> monsterDictionary)
		{
			characterList = charList;
			item_dictionary = itemDictionary;
			monster_dictionary = monsterDictionary;
			monsterList = new List<Monster>();
			characterDeadList = new List<Character>();
			dungeonLevel = 0;
			rand = new Random();

        }

		public string IncrementDungeonLevel()
		{
			dungeonLevel++;
			return "********* Entering Dungeon Level = " + dungeonLevel + " *********";
		}

        public bool CharacterIsFaster()
        {
            if (characterList.Count > 0 && monsterList.Count > 0)
            {
                if (characterList[0].Speed >= monsterList[0].Speed)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public void sortListBySpeed(List<ICreature> creature)
        {
            List<Character> sorted = new List<Character>();
            //while (characterList.Count > 0)
            //{

            //}
        }

		public string generateMonsterList(int dungeonLevel)
		{

			int num_monsters = rand.Next(1, 4) + dungeonLevel;
			for (int i = 0; i < num_monsters; i++)
			{
				monsterList.Add(monster_dictionary[rand.Next(monster_dictionary.Count)]);
				monsterList[i].setMonsterLevel(dungeonLevel);
                if (rand.Next(99) % 2 == 0)
                    monsterList[i].addItem(new Item(item_dictionary[rand.Next(item_dictionary.Count)]));
            }
			return "There are " + num_monsters.ToString() + " monsters in this dungeon.";
		}

		public List<Monster> currentMonsterList()
		{
			return monsterList;
		}

		public bool areAnyCharactersAlive()
		{
			if (characterList.Count > 0)
				return true;
			else
				return false;
		}

		public bool areAnyMonstersAlive()
		{
			if (monsterList.Count > 0)
				return true;
			else
				return false;
		}

        public void PlayerStatusReset()
        {
            foreach (Character hero in characterList)
            {
                hero.ResetStatus();
            }
        }

        public float dodgePercentile(int Agility, int oppDex, int oppLvL)
        {

            return (float)Agility / (float)(oppDex * oppLvL + 100) ;
        }

        public bool dodged(float dodge, float chance)
        {
            return chance <= dodge;
        }


        public List<string> Volley()
		{
            //pop front hero and front monster off their respective queues
			List<string> actions = new List<string>();
			string action;
            int dmg = 0;
            float dodge_rating;
            bool characterGoesFirst = CharacterIsFaster();
			Character hero = characterList[0];
			Monster monster = monsterList[0];
			characterList.RemoveAt(0);
			monsterList.RemoveAt(0);
            //Whoever has fastest speed gets first hit, benefit if kills opponent -> returns to queue with no damage taken
            if (true)//characterGoesFirst)
            {
                //Hero attacks monster
                float dodgeRating = dodgePercentile(monster.Agility, hero.Dexterity, hero.Level);
                if (!dodged(dodgeRating, (float)(rand.Next(1, 1000) / 1000F)))// dodge failed, take hit
                {
                    dmg = monster.takeDamage(hero.Attack());
                    
                    actions.Add(monster.Name + " took "+ dmg +" damage from " + hero.Name);
                }
                else// successfully dodged
                {
                    actions.Add(monster.Name + " dodged attack from " + hero.Name);
                }
                //monster attacks hero, if monster is still alive
                if (monster.isDead())
                {
                    action = hero.Name + " killed " + monster.Name + " with " + dmg + " damage.";
                    actions.Add(action);
                    if (monster.hasItem())
                    {
                        Item droppedItem = monster.discardItem();
                        if (hero.evaluateNewItem(droppedItem))
                            action = hero.Name + " equipped " + droppedItem.name + ", was dropped by " + monster.Name;
                        actions.Add(action);
                    }
                    //character pick up item off of monster dead body
                    if (hero.addExperience(monster.xpValue))
                    {
                        actions.Add(hero.Name + " Leveled up!!!!!!!");
                    }
                    characterList.Add(hero);
                }
                else//monster still alive, monster attacks hero
                {
                    dodgeRating = dodgePercentile(monster.Agility, hero.Dexterity, hero.Level);
                    if (!dodged(dodgeRating, (float)(rand.Next(1, 1000) / 1000F)))// dodge failed, take hit
                    {
                        dmg = hero.takeDamage(monster.Attack());
                        actions.Add(monster.Name + " hit " + hero.Name + " with " + dmg + " damage.");
                    }
                    else
                    {
                        actions.Add(hero.Name + " dodged attack from " + monster.Name);
                    }
                    if (hero.isDead())
                    {
                        action = monster.Name + " killed " + hero.Name + " with " + dmg + " damage.";
                        actions.Add(action);
                        action = "**************" + hero.Name + " died**************";
                        actions.Add(action);
                        characterDeadList.Add(hero);
                        CrossVibrate.Current.Vibration();
                    }
                    else
                    {
                        characterList.Add(hero);
                    }
                    monsterList.Add(monster);
                }
            }
            else//monster goes first
            {

            }
			return actions;
		}
	}
}

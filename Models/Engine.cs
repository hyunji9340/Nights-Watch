﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Vibrate;
using GroupProject_DD.Models;

namespace GroupProject_DD
{
    public class Engine
    {
        /***********************resource declarations***************************/
        public List<ICreature> characterList;
        public List<ICreature> monsterList;
        public List<ICreature> characterDeadList;
        public List<ICreature> deadMonsterList;
        public List<Item> item_dictionary;
        public List<Monster> monster_dictionary;
        public int dungeonLevel;
        public bool isGameOver;
        Random rand;
        public Player currentPlayer;
        private PlayerController playerController;

        public Engine(List<ICreature> charList, List<Item> itemDictionary, List<Monster> monsterDictionary, Player currentPlayer)
        {
            characterList = charList;
            item_dictionary = itemDictionary;
            monster_dictionary = monsterDictionary;
            deadMonsterList = new List<ICreature>();
            monsterList = new List<ICreature>();
            characterDeadList = new List<ICreature>();
            dungeonLevel = 0;
            rand = new Random();
            this.currentPlayer = currentPlayer;
            this.playerController = new PlayerController();
            this.isGameOver = false;
        }

        public string IncrementDungeonLevel()
        {
            if (dungeonLevel < 5)
            {
                dungeonLevel++;
                return "********* Entering Dungeon Level = " + dungeonLevel + " *********";
            }
            else
            {
                // after dungeon level 5, the game will over
                this.isGameOver = true;
                return "";
            }
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

        public void sortLists()
        {
            characterList = sortListBySpeed(characterList);
            monsterList = sortListBySpeed(monsterList);
        }

        public List<ICreature> sortListBySpeed(List<ICreature> creature)
        {
            List<ICreature> sorted = new List<ICreature>();
            while (creature.Count > 0)
            {
                int HighestSpeedIndex = 0;
                for (int i = 0; i < creature.Count; i++)
                    if (creature[i].Speed >= creature[HighestSpeedIndex].Speed)
                        HighestSpeedIndex = i;
                ICreature holder = creature[HighestSpeedIndex];
                creature.RemoveAt(HighestSpeedIndex);
                sorted.Add(holder);
            }
            creature = sorted;
            return creature;
        }

        public string generateMonsterList(int dungeonLevel)
        {

            int num_monsters = rand.Next(1, 4) + dungeonLevel;
            for (int i = 0; i < num_monsters; i++)
            {
                Monster monster = new Monster(monster_dictionary[rand.Next(monster_dictionary.Count)]);
                monster.setMonsterLevel(dungeonLevel + rand.Next(1, 3));
                if (rand.Next(99) % 1 == 0)//percentage to spawn with an item
                    monster.addItem(new Item(item_dictionary[rand.Next(item_dictionary.Count)]));
                monsterList.Add(monster);
            }
            return "There are " + num_monsters.ToString() + " monsters in this dungeon.";
        }

        public List<ICreature> currentMonsterList()
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

            return (float)Agility / (float)(oppDex * oppLvL + 100);
        }

        public bool dodged(float dodge, float chance)
        {
            return chance <= dodge;
        }


        public List<string> Volley(bool characterAttackingFirst)
        {
            //pop front hero and front monster off their respective queues
            List<string> actions = new List<string>();
            string action;
            float dodge_rating;
            ICreature hero = characterList[0] as Character;
            ICreature monster = monsterList[0] as Monster;
            characterList.RemoveAt(0);
            monsterList.RemoveAt(0);
            bool DidDefenderDie;
            if (characterAttackingFirst)
            {
                DidDefenderDie = Swing(hero, monster, actions);
                //Attack back if not dead
                if (!DidDefenderDie)
                {
                    Swing(monster, hero, actions);
                }
            }
            else
            {
                DidDefenderDie = Swing(monster, hero, actions);
                //Attack back if not dead
                if (!DidDefenderDie)
                {
                    Swing(hero, monster, actions);
                }
            }
            //attacks complete
            if (hero.isDead())
            {
                action = "**************" + hero.Name + " died**************";
                actions.Add(action);
                hero.curHealth = 0;
                characterDeadList.Add(hero);
                CrossVibrate.Current.Vibration();
            }

            if (monster.isDead())
            {
                deadMonsterList.Add(monster);
                Monster _monster = monster as Monster;
                Character _hero = hero as Character;
                _hero.monstersKilled++;
                if (_monster.hasItem())
                {
                    Item droppedItem = _monster.discardItem();
                    if (_hero.evaluateNewItem(droppedItem))
                    {
                        action = hero.Name + " equipped " + droppedItem.name + ", was dropped by " + monster.Name;
                        actions.Add(action);
                    }
                }
                //character pick up item off of monster dead body
                if (_hero.addExperience(monster.Experience))
                {
                    actions.Add(_hero.Name + " Leveled up!!!!!!!");
                }
            }


            if (!monster.isDead())
            {
                monsterList.Add(monster);
            }

            if (!hero.isDead())
            {
                characterList.Add(hero);
            }
            return actions;
        }

        private bool Swing(ICreature Attacker, ICreature Defender, List<string> actions)
        {
            int dmg = 0;
            float dodgeRating = dodgePercentile(Attacker.Agility, Defender.Dexterity, Defender.Level);
            if (!dodged(dodgeRating, (float)(rand.Next(1, 1000) / 1000F)))// dodge failed, take hit
            {
                dmg = Defender.takeDamage(Attacker.Attack());
                if (Defender.isDead())
                {
                    actions.Add(Attacker.Name + " killed " + Defender.Name + " with " + dmg + " damage");
                    return true;
                }
                else
                {
                    actions.Add(Defender.Name + " took " + dmg + " damage from " + Attacker.Name);
                }
            }
            else// successfully dodged
            {
                actions.Add(Defender.Name + " dodged attack from " + Attacker.Name);
            }
            return false;
        }

        // method that calculates total earned point. This method is called when either all monster or all characters are dead
        public void PointsEearned()
        {
            int totalPoints = 0;
            for (int i = 0; i < deadMonsterList.Count; i++)
            {
                totalPoints += (deadMonsterList[i].Level * 10);
            }

            this.currentPlayer.TotalScore = totalPoints;
            this.playerController.SavePlayer(currentPlayer);
        }
    }
}

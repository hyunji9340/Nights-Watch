using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Vibrate;
using GroupProject_DD.Models;
using System.Diagnostics;

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
        private List<BattleEffects> battleEvent_dictionary;
        public int dungeonLevel;
        public bool isGameOver;
        Random rand;
        public Player currentPlayer;
        private PlayerController playerController;
        private Settings settings;
		private Item currentlyEquippedItem;

        public Engine(List<ICreature> charList, List<Item> itemDictionary, List<Monster> monsterDictionary, Player currentPlayer, Settings IncomingSettings)
        {
            settings = IncomingSettings;
            characterList = charList;
            item_dictionary = itemDictionary;
            monster_dictionary = monsterDictionary;
            battleEvent_dictionary = new List<BattleEffects>();
            deadMonsterList = new List<ICreature>();
            monsterList = new List<ICreature>();
            characterDeadList = new List<ICreature>();
            dungeonLevel = 0;
            rand = new Random();
            this.currentPlayer = currentPlayer;
            this.playerController = new PlayerController();
            this.isGameOver = false;
			this.currentlyEquippedItem = null;
        }
        //for unit testing
        public Engine(List<ICreature> charList, List<Item> itemDictionary, List<Monster> monsterDictionary)
        {
            characterList = charList;
            item_dictionary = itemDictionary;
            monster_dictionary = monsterDictionary;
            battleEvent_dictionary = new List<BattleEffects>();
            deadMonsterList = new List<ICreature>();
            monsterList = new List<ICreature>();
            characterDeadList = new List<ICreature>();
            dungeonLevel = 0;
            rand = new Random();
            this.isGameOver = false;
            this.currentlyEquippedItem = null;
        }

        public int CheckCritical()
        {
            int critChance = rand.Next(1, 21);
            if (critChance == 1)
                return -1;
            else if (critChance == 20)
                return 1;
            else 
                return 0;
        }

        public string IncrementDungeonLevel()
        {
            if (dungeonLevel < 12)
            {
                dungeonLevel++;
                return "********* Entering Dungeon Level = " + dungeonLevel + " *********";
            }
            else
            {
                // after dungeon level 5, the game will over
                this.isGameOver = true;
                return "********* The Heros cleared out the dungeon, GAME OVER!!! *********";
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

        public string generateMonsterList(int DungeonLevel)
        {
            this.dungeonLevel = DungeonLevel;
            int num_monsters = rand.Next(1, 4) + DungeonLevel;
            for (int i = 0; i < num_monsters; i++)
            {

                Monster monster = new Monster(monster_dictionary[rand.Next(monster_dictionary.Count)]);
                if (DungeonLevel > 2)
                    monster.setMonsterLevel(DungeonLevel + rand.Next(DungeonLevel, DungeonLevel* DungeonLevel));
                else
                    monster.setMonsterLevel(DungeonLevel + rand.Next(1, 3));
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

        public void CastAll(ICreature Attacker, ICreature Defender, List<String> actions)
        {
            int crit = CheckCritical();
            bool defenderDead = false;
            if(settings.EveryCriticalHit == true)
            {
                crit = 1;
            }
            actions.Add(Attacker.Name + " used magic and attacked all the monsters!");
            if (crit == 1)
            {
                actions.Add(Attacker.Name + " cast a critical hit!");
                int dmg = Defender.takeDamage(2 * Attacker.generateAttackDmg());
                actions.Add(Defender.Name + " took " + dmg + " damage");
            }
            else
            {
                int dmg = (Defender.takeDamage(Attacker.generateAttackDmg()));
                actions.Add(Defender.Name + " took " + dmg + " damage");
            }
            foreach (ICreature otherMonster in monsterList)
            {
                if(crit == 1)
                {
                    int dmg = otherMonster.takeDamage(2*Attacker.generateAttackDmg());
                    actions.Add(otherMonster.Name + " took " + dmg + " damage");
                }
                else
                {
                    int dmg = (otherMonster.takeDamage(Attacker.generateAttackDmg()));
                    actions.Add(otherMonster.Name + " took " + dmg + " damage");
                }
            }
            if (Defender.isDead())
            {
                defenderDead = true;
                deadMonsterList.Add(Defender);
                Monster _monster = Defender as Monster;
                Character _hero = Attacker as Character;
                _hero.monstersKilled++;
                if (_monster.hasItem())
                {
                    Item droppedItem = _monster.discardItem();

                    //if the item type is healing, heals player by damaging for -rating amount.
                    if (droppedItem.BodyPart == "HEALING")
                    {
                        if (settings.Healing == true)
                        {
                            int healedPoints = -1 * droppedItem.Tier;
                            Attacker.takeDamage(healedPoints);
                            actions.Add(Attacker.Name + " used " + droppedItem.Name + " dropped by" + Defender.Name);
                            actions.Add(Attacker.Name + " healed by " + droppedItem.Tier + " points");
                        }
                    }
                    else if (droppedItem.BodyPart == "MAGICALL" || droppedItem.BodyPart == "MAGICDIRECT")
                    {
                        if (settings.MagicUsage == true)
                        {
                            bool test = _hero.evaluateNewItem(droppedItem);
                            string action = _hero.Name + " equipped " + droppedItem.Name + ", was dropped by " + _monster.Name;
							currentlyEquippedItem = droppedItem;
                            actions.Add(action);
                        }
                    }
                    else if(_hero.evaluateNewItem(droppedItem) == true)
                    {
                        actions.Add(Attacker.Name + " killed " + Defender.Name);
                        string action = Attacker.Name + " equipped " + droppedItem.Name + ", was dropped by " + Defender.Name;
						currentlyEquippedItem = droppedItem;
                        actions.Add(action);
                    }
                }
                //character pick up item off of monster dead body
                if (_hero.addExperience(Defender.Experience))
                {
                    actions.Add(_hero.Name + " Leveled up!!!!!!!");
                }
            }

            for (int i = 0; i < monsterList.Count; i++)
            {
                ICreature tempMonster = monsterList[0] as Monster;
                monsterList.RemoveAt(0);
                if (defenderDead == false)
                {
                    monsterList.Add(Defender);
                    defenderDead = true;
                }
                if (tempMonster.isDead())
                {
                    deadMonsterList.Add(tempMonster);
                    Monster _monster = tempMonster as Monster;
                    Character _hero = Attacker as Character;
                    _hero.monstersKilled++;
                    if (_monster.hasItem())
                    {
                        Item droppedItem = _monster.discardItem();
                        //if the item type is healing, heals player by damaging for -rating amount.
                        if (droppedItem.BodyPart == "HEALING")
                        {
                            if(settings.Healing == true)
                            {
                                actions.Add(_hero.Name + " used " + droppedItem.Name + " dropped by" + _monster.Name);
                                int healedPoints = -1 * droppedItem.Tier;
                                _hero.takeDamage(healedPoints);
                                actions.Add(_hero.Name + " healed by " + droppedItem.Tier + " points");
                            }
                            
                        }
                        else if (droppedItem.BodyPart == "MAGICALL" || droppedItem.BodyPart == "MAGICDIRECT")
                        {
                            if (settings.MagicUsage == true)
                            {
                                bool test = _hero.evaluateNewItem(droppedItem);
                                string action = _hero.Name + " equipped " + droppedItem.Name + ", was dropped by " + _monster.Name;
                                actions.Add(action);
								currentlyEquippedItem = droppedItem;
                            }
                        }
                        else
                        {
                            bool test = _hero.evaluateNewItem(droppedItem);
                            string action = _monster.Name + " equipped " + droppedItem.Name + ", was dropped by " + _monster.Name;
                            actions.Add(action);
							currentlyEquippedItem = droppedItem;
                        }
                    }
                    //character pick up item off of monster dead body
                    if (_hero.addExperience(_monster.Experience))
                    {
                        actions.Add(_hero.Name + " Leveled up!!!!!!!");
                    }
                }
                else
                    monsterList.Add(tempMonster);
            }
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
            bool DidDefenderDie = false;
            Debug.WriteLine("Hero Speed: {0}", hero.Speed);
            Debug.WriteLine("Monster Speed: {0}", monster.Speed);
            Debug.WriteLine("Char initiative: {0}", characterAttackingFirst);
            if (characterAttackingFirst)
            {
                Character mage = hero as Character;
                Debug.WriteLine("Tome: {0}", mage.Inventory[Bodypart.MagicAll].Name);
                if (mage.Inventory[Bodypart.MagicAll].Name != "Empty" && settings.MagicUsage == true)
                {
                    CastAll(hero, monster, actions);
                    if (monsterList.Count != 0)
                        monster = monsterList[0] as Monster;
                    else
                        DidDefenderDie = true;
                }                    
                else
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
                Debug.WriteLine("Monster name: {0}", _monster.Name);
                Debug.WriteLine("Monster item: {0}", _monster.hasItem());
                Character _hero = hero as Character;
                _hero.monstersKilled++;
                if (_monster.hasItem())
                {
                    Item droppedItem = _monster.discardItem();
                    Debug.WriteLine("dropped item: {0}", droppedItem.Name);
                    Debug.WriteLine("dropped type: {0}", droppedItem.BodyPart);
                    //if the item type is healing, heals player by damaging for -rating amount.
                    if(droppedItem.BodyPart == "HEALING")
                    {
                        if (settings.Healing == true)
                        {
                            actions.Add(hero.Name + " used " + droppedItem.Name + " dropped by" + monster.Name);
                            int healedPoints = -1 * droppedItem.Tier;
                            hero.takeDamage(healedPoints);
                            actions.Add(hero.Name + " healed by " + droppedItem.Tier + " points");
                        }
                        
                    }
                    else if(droppedItem.BodyPart == "MAGICALL" || droppedItem.BodyPart == "MAGICDIRECT")
                    {
                        if(settings.MagicUsage == true)
                        {
                            bool test = _hero.evaluateNewItem(droppedItem);
                            action = hero.Name + " equipped " + droppedItem.Name + ", was dropped by " + monster.Name;
                            actions.Add(action);
							currentlyEquippedItem = droppedItem;
                        }
                    }
                    else
                    {
                        bool test = _hero.evaluateNewItem(droppedItem);
                        action = hero.Name + " equipped " + droppedItem.Name + ", was dropped by " + monster.Name;
                        actions.Add(action);
						currentlyEquippedItem = droppedItem;
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
            bool charUsesFists = false;
            float dodgeRating = dodgePercentile(Attacker.Agility, Defender.Dexterity, Defender.Level);
            if (!dodged(dodgeRating, (float)(rand.Next(1, 1000) / 1000F)))// dodge failed, take hit
            {
                //calls CheckCritical to check for critical hit (1), critical miss (-1), or neither (0)
                int attackerCrit = CheckCritical();
                if(settings.EveryCriticalHit == true)
                {
                    attackerCrit = 1;
                }
                else if(settings.EveryCriticalMiss == true)
                {
                    attackerCrit = -1;
                }
                //special case needed to evaluate unarmed characters
                if (Attacker is Character)
                {
                    Character hero = Attacker as Character;
                    if (hero.Inventory[Bodypart.AttkArm].Name == "Empty")
                    {
                        charUsesFists = true;
                    }
                }
                if (charUsesFists) //implement fist damage
                {
                    if (attackerCrit == 1) {
                        actions.Add(Attacker.Name + " scored a Critical Hit!");
                        dmg = (Defender.takeDamage(2*0));
                    }
                    else
                        dmg = (Defender.takeDamage(0));
                    if (Defender.isDead())
                    {
                        actions.Add(Attacker.Name + " killed " + Defender.Name + " with their bare hands... =O");
                        return true;
                    }
                    else
                    {
                        actions.Add(Attacker.Name + " punched " + Defender.Name +  " with their fists for " + dmg + " damage.");
                    }
                }
                else //normal operation
				{
					//double damage if crit
					if (Attacker is Character && attackerCrit == -1)
					{
                        
						//randomly picks a number between 0 and 7 to pick an item to break upon crit miss
						Character pc = Attacker as Character;
						int discardedIndex = rand.Next(0, 7);
						if (discardedIndex == 0 && pc.Inventory[Bodypart.Head].Name != "Empty")
						{
							Item brokenItem = new Item(pc.Inventory[Bodypart.Head]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
							if (currentlyEquippedItem != null) currentlyEquippedItem = null;
						}
						if (discardedIndex == 1 && pc.Inventory[Bodypart.AttkArm].Name != "Empty")
						{
							Item brokenItem = new Item(pc.Inventory[Bodypart.AttkArm]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
							if (currentlyEquippedItem != null) currentlyEquippedItem = null;
						}
						if (discardedIndex == 2 && pc.Inventory[Bodypart.DefArm].Name != "Empty")
						{
							Item brokenItem = new Item(pc.Inventory[Bodypart.DefArm]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
							if (currentlyEquippedItem != null) currentlyEquippedItem = null;
						}
						if (discardedIndex == 3 && pc.Inventory[Bodypart.Torso].Name != "Empty")
						{
							Item brokenItem = new Item(pc.Inventory[Bodypart.Torso]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
							if (currentlyEquippedItem != null) currentlyEquippedItem = null;
						}
						if (discardedIndex == 4 && pc.Inventory[Bodypart.Feet].Name != "Empty")
						{
							Item brokenItem = new Item(pc.Inventory[Bodypart.Feet]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
							if (currentlyEquippedItem != null) currentlyEquippedItem = null;
							pc.discardItem(brokenItem);
						}
						if (discardedIndex == 5 && pc.Inventory[Bodypart.MagicDirect].Name != "Empty")
						{
							Item brokenItem = new Item(pc.Inventory[Bodypart.MagicDirect]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
							pc.discardItem(brokenItem);
							if (currentlyEquippedItem != null) currentlyEquippedItem = null;
						}
						if (discardedIndex == 6 && pc.Inventory[Bodypart.MagicAll].Name != "Empty")
						{
							Item brokenItem = new Item(pc.Inventory[Bodypart.MagicAll]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
							pc.discardItem(brokenItem);
							if (currentlyEquippedItem != null) currentlyEquippedItem = null;
						}
					}
					else if (attackerCrit == 1)
					{
						actions.Add(Attacker.Name + " scored a Critical Hit!");

						if (settings.ItemUsage && Attacker is Character) 
						{
							
							if (currentlyEquippedItem != null)
							{
								if (currentlyEquippedItem.Usage > 0)
								{
									actions.Add(currentlyEquippedItem.Name + " is still okay to use.");
								}
								else
								{
									actions.Add(currentlyEquippedItem.Name + " was used too many times! It's broke!");
									currentlyEquippedItem = null;
								}
							}
						}
						dmg = Defender.takeDamage(2 * Attacker.generateAttackDmg());
					}
					else
					{
						if (settings.ItemUsage)
						{
							if (currentlyEquippedItem != null)
							{
								if (currentlyEquippedItem.Usage > 0)
								{
									actions.Add(currentlyEquippedItem.Name + " is still okay to use.");
								}
								else
								{
									actions.Add(currentlyEquippedItem.Name + " was used too many times! It's broke!");
									currentlyEquippedItem = null;
								}
							}
						}
                        dmg = Defender.takeDamage(Attacker.generateAttackDmg());
					}

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

        public void populateBattleEvents()
        {
        }
    }
}

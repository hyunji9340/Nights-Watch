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
		bool isEquipped = false;

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

        public void CastAll(ICreature Attacker, List<String> actions)
        {
            int crit = CheckCritical();
            if(settings.EveryCritical == true)
            {
                crit = 1;
            }
            actions.Add(Attacker.Name + " cast a Critical Hit!");
            foreach (ICreature Defender in monsterList)
            {
                if(crit == 1)
                {
                    int dmg = Defender.takeDamage(2*Attacker.Attack());
                    actions.Add(Defender.Name + " took " + dmg + " damage");
                }
                else
                {
                    int dmg = (Defender.takeDamage(Attacker.Attack()));
                    actions.Add(Defender.Name + " took " + dmg + " damage");
                }
            }
            for(int i = 0; i < monsterList.Count; i++)
            {
                ICreature tempMonster = monsterList[0] as Monster;
                monsterList.RemoveAt(0);
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
                        if (droppedItem.BodyPart == "HEALING" && settings.Healing == true)
                        {
                            int healedPoints = -1 * droppedItem.Tier;
                            Attacker.takeDamage(healedPoints);
                            actions.Add(Attacker.Name + " used " + droppedItem.Name + " dropped by" + tempMonster.Name);
                            actions.Add(Attacker.Name + " healed by " + droppedItem.Tier + " points");
                        }
                        else if (_hero.evaluateNewItem(droppedItem))
                        {
							Debug.WriteLine("IN HERO_EVALUATENEWITEM(DROPPEDITEM) droppedItem0: " + droppedItem.Name + " usage: " + droppedItem.Usage);
                            string action = Attacker.Name + " equipped " + droppedItem.Name + ", was dropped by " + tempMonster.Name;
                            actions.Add(action);
							isEquipped = true;
                        }
                    }
                    //character pick up item off of monster dead body
                    if (_hero.addExperience(tempMonster.Experience))
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
                    //if the item type is healing, heals player by damaging for -rating amount.
                    if(droppedItem.BodyPart == "HEALING" && settings.Healing == true)
                    {
                        actions.Add(hero.Name + " used " + droppedItem.Name + " dropped by" + monster.Name);
                        int healedPoints = -1*droppedItem.Tier;
                        hero.takeDamage(healedPoints);
                        actions.Add(hero.Name + " healed by " + droppedItem.Tier + " points");
                    }
                    else if (_hero.evaluateNewItem(droppedItem))
                    {
                        action = hero.Name + " equipped " + droppedItem.Name + ", was dropped by " + monster.Name;
                        actions.Add(action);
						isEquipped = true;
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
                if(settings.EveryCritical == true)
                {
                    attackerCrit = 1;
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
						//randomly picks a number between 0 and 5 to pick an item to break upon crit miss
						Character pc = Attacker as Character;
						int discardedIndex = rand.Next(0, 5);
						Item brokenItem = new Item(pc.Inventory[Bodypart.Head]);
						if (discardedIndex == 0 && pc.Inventory[Bodypart.Head].Name != "Empty")
						{
							brokenItem = new Item(pc.Inventory[Bodypart.Head]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
						}
						if (discardedIndex == 1 && pc.Inventory[Bodypart.AttkArm].Name != "Empty")
						{
							brokenItem = new Item(pc.Inventory[Bodypart.AttkArm]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
						}
						if (discardedIndex == 2 && pc.Inventory[Bodypart.DefArm].Name != "Empty")
						{
							brokenItem = new Item(pc.Inventory[Bodypart.DefArm]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
						}
						if (discardedIndex == 3 && pc.Inventory[Bodypart.Torso].Name != "Empty")
						{
							brokenItem = new Item(pc.Inventory[Bodypart.Torso]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
						}
						if (discardedIndex == 4 && pc.Inventory[Bodypart.Feet].Name != "Empty")
						{
							brokenItem = new Item(pc.Inventory[Bodypart.Feet]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
							pc.discardItem(brokenItem);
						}
						if (discardedIndex == 5 && pc.Inventory[Bodypart.MagicDirect].Name != "Empty")
						{
							brokenItem = new Item(pc.Inventory[Bodypart.MagicDirect]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
							pc.discardItem(brokenItem);
						}
						if (discardedIndex == 6 && pc.Inventory[Bodypart.MagicAll].Name != "Empty")
						{
							brokenItem = new Item(pc.Inventory[Bodypart.MagicAll]);
							actions.Add(Attacker.Name + " fumbled and scored a Critical Miss.");
							actions.Add("Their " + brokenItem.Name + " broke!");
							pc.discardItem(brokenItem);
						}
					}
					else if (attackerCrit == 1)
					{
						actions.Add(Attacker.Name + " scored a Critical Hit!");
						dmg = Defender.takeDamage(2 * Attacker.Attack());
					}
					else
					{
						// THIS IS THE PART WHERE I SUPPOSE TO CALL FUNCTIONS THAT ARE RELATED TO STEP 6
						/*if (Attacker is Character && isEquipped)
						{
							Character pc = Attacker as Character;
							CheckItemUsage(pc, actions);
							DecreaseItemUsage(pc);
						}*/
	
						dmg = Defender.takeDamage(Attacker.Attack());
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

		// implmented for hackathon -> FUNCTION RELATED TO STEP 6
		public bool CheckItemUsage(Character currentCharacter, List<string> actions)
		{

			if (currentCharacter.Inventory[Bodypart.Head] != null)
			{
				return CheckItemUsageHelper(currentCharacter, currentCharacter.Inventory[Bodypart.Head], actions);
			}
			if (currentCharacter.Inventory[Bodypart.AttkArm] != null)
			{
				return CheckItemUsageHelper(currentCharacter, currentCharacter.Inventory[Bodypart.AttkArm], actions);
			}
			if (currentCharacter.Inventory[Bodypart.DefArm] != null)
			{
				return CheckItemUsageHelper(currentCharacter, currentCharacter.Inventory[Bodypart.DefArm], actions);
			}
			if (currentCharacter.Inventory[Bodypart.Torso] != null)
			{
				return CheckItemUsageHelper(currentCharacter, currentCharacter.Inventory[Bodypart.Torso], actions);
			}
			if (currentCharacter.Inventory[Bodypart.Feet] != null)
			{
				return CheckItemUsageHelper(currentCharacter, currentCharacter.Inventory[Bodypart.Feet], actions);
			}
			if (currentCharacter.Inventory[Bodypart.MagicAll] != null)
			{
				return CheckItemUsageHelper(currentCharacter, currentCharacter.Inventory[Bodypart.MagicAll], actions);
			}
			if (currentCharacter.Inventory[Bodypart.MagicDirect] != null) 
			{
				return CheckItemUsageHelper(currentCharacter, currentCharacter.Inventory[Bodypart.MagicDirect], actions);
			}
			return true;
		}

		// FUNCTION RELATED TO STEP 6
		public bool CheckItemUsageHelper(Character currentCharacter, Item item, List<string> actions)
		{


			if (item.Name != "Empty" && item.Usage <= 0 && item.Name.Length > 0)
			{
				currentCharacter.discardItem(item);
				currentCharacter.equipItem(item);
				actions.Add(currentCharacter.Name + "'s Item " + item.Name + " is broken. Used too many times!");
				return true;
			}
			else if (item.Name != "Empty" && item.Usage > 0 && item.Name.Length > 0)
			{
				actions.Add(currentCharacter.Name + "'s Item " + item.Name + " is still good to use!");
				return false;
			}
			return false;
		}

		// FUNCTION RELATED TO STEP 6
		public void DecreaseItemUsage(Character currentCharacter)
		{
			if (currentCharacter.Inventory[Bodypart.Head] != null)
			{
				DecreaseItemUsageHelper(currentCharacter.Inventory[Bodypart.Head]);
			}
			if (currentCharacter.Inventory[Bodypart.AttkArm] != null)
			{
				DecreaseItemUsageHelper(currentCharacter.Inventory[Bodypart.AttkArm]);
			}
			if (currentCharacter.Inventory[Bodypart.DefArm] != null)
			{
				DecreaseItemUsageHelper(currentCharacter.Inventory[Bodypart.DefArm]);
			}
			if (currentCharacter.Inventory[Bodypart.Torso] != null)
			{
				DecreaseItemUsageHelper(currentCharacter.Inventory[Bodypart.Torso]);
			}
			if (currentCharacter.Inventory[Bodypart.Feet] != null)
			{
				DecreaseItemUsageHelper(currentCharacter.Inventory[Bodypart.Feet]);
			}
		}

		// FUNCTION RELATED TO STEP 6
		public void DecreaseItemUsageHelper(Item item)
		{
			if (item.Name != "Empty" && item.Name.Length > 0)
			{
				item.Usage--;
				//actions.Add(currentCharacter.Name + "'s Item " + item.Name + " is still good to use!");
			}
		}

        public void populateBattleEvents()
        {

        }
    }
}

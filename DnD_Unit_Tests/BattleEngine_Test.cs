using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GroupProject_DD;
using GroupProject_DD.Models;

namespace DnD_Unit_Tests
{
    [TestClass]
    public class BattleEngine_Test
    {
        private List<Item> item_dictionary;
        private List<Monster> monster_dictionary;
        Engine BattleEngine;
        public List<ICreature> CharacterList { get; private set; }

        [TestInitialize]
        public void EngineStartup()
        {
            CharacterList = new List<ICreature>()
            {
                new Character("Greg the Chancellor"),
                new Character("Sally the Destroyer"),
                new Character("Spencer the Barbarian"),
                new Character("Andrew the Jester")
            };

            item_dictionary = new List<Item>()
            {
                new Item("", "Helmet", "It's story", 7, Bodypart.Head, "STRENGTH", 20),
                new Item("", "Sword", "It's sharp", 7, Bodypart.AttkArm, "STRENGTH", 20)
            };
            monster_dictionary = new List<Monster>()
            {
                new Monster(),
                new Monster()
            };

            BattleEngine = new Engine(CharacterList, item_dictionary, monster_dictionary);
        }

        [TestMethod]
        public void MonsterGeneration()
        {
            BattleEngine.generateMonsterList(1);
            Assert.IsTrue(BattleEngine.currentMonsterList().Count > 0);
        }

        [TestMethod]
        public void DungeonIncrementing()
        {
            int current_dungeon_level = BattleEngine.dungeonLevel;
            BattleEngine.IncrementDungeonLevel();
            Assert.IsTrue(current_dungeon_level < BattleEngine.dungeonLevel);
        }

        [TestMethod]
        public void CharacterIsFasterThanMonster()
        {
            BattleEngine.generateMonsterList(1);
            BattleEngine.characterList[0].Speed = 20;
            BattleEngine.monsterList[0].Speed = 15;
            Assert.IsTrue(BattleEngine.CharacterIsFaster());
        }

        [TestMethod]
        public void MonsterIsFasterThanCharacter()
        {
            BattleEngine.generateMonsterList(1);
            BattleEngine.characterList[0].Speed = 10;
            BattleEngine.monsterList[0].Speed = 15;
            Assert.IsFalse(BattleEngine.CharacterIsFaster());
        }


        [TestMethod]
        public void CharactersAreAlive()
        {
            Assert.IsTrue(BattleEngine.areAnyCharactersAlive());
        }

        [TestMethod]
        public void CharactersAreDead()
        {
            while (BattleEngine.characterList.Count > 0)
                BattleEngine.characterList.RemoveAt(0);
            Assert.IsFalse(BattleEngine.areAnyCharactersAlive());
        }

        [TestMethod]
        public void MonstersAreAlive()
        {
            BattleEngine.generateMonsterList(2);
            Assert.IsTrue(BattleEngine.areAnyMonstersAlive());
        }

        [TestMethod]
        public void MonstersAreDead()
        {
            while (BattleEngine.monsterList.Count > 0)
                BattleEngine.monsterList.RemoveAt(0);
            Assert.IsFalse(BattleEngine.areAnyMonstersAlive());
        }

        [TestMethod]
        public void CharacterStatsReset()
        {
            Character hero = BattleEngine.characterList[0] as Character;
            hero.curHealth = 0;
            BattleEngine.PlayerStatusReset();
            Assert.IsTrue(hero.curHealth > 0);
        }

        [TestMethod]
        public void DodgedAttack()
        {
            Assert.IsTrue(BattleEngine.dodged(0.5f, 0.4f));
        }

        [TestMethod]
        public void DidntDodgeAttack()
        {
            Assert.IsFalse(BattleEngine.dodged(0.6f, 0.7f));
        }
    }
}

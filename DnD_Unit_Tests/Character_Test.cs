using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroupProject_DD;


namespace DnD_Unit_Tests
{
    [TestClass]
    public class Character_Test
    {
        [TestMethod]
        public void isCharacterAlive()
        {
            Character hero = new Character();
            Assert.IsFalse(hero.isDead());
        }

        [TestMethod]
        public void isCharacterDead()
        {
            Character hero = new Character();
            hero.takeDamage(25);
            Assert.IsTrue(hero.isDead());
        }

        [TestMethod]
        public void CharacterAliveAfterTakingDamage()
        {
            Character hero = new Character();
            hero.takeDamage(10);
            Assert.IsFalse(hero.isDead());
        }

        [TestMethod]
        public void characterTakesDamageAndDies()
        {
            Character hero = new Character();
            int health_placeholder = hero.Vitality;
            hero.takeDamage(10);
            Assert.AreNotSame(hero.Vitality, health_placeholder);
        }

        [TestMethod]
        public void characterLevelUp()
        {
            Character hero = new Character();
            hero.levelUp();
            Assert.IsTrue(hero.Level > 1);
        }

        [TestMethod]
        public void characterLevelUpWithXP()
        {
            Character hero = new Character();
            int XP_placeholder = hero.Experience;
            hero.addExperience(200);
            Assert.IsTrue(hero.Level > 1);
        }

        [TestMethod]
        public void characterKilledsomeMonsters()
        {
            Character hero = new Character();
            hero.monstersKilled++;
            Assert.IsTrue(hero.monstersKilled > 0);
        }

        //[TestMethod]
        //public void characterAddedItem()
        //{
        //    Character hero = new Character();
        //    Item item = new Item("sword", "it's a sword", 1, "sword", 1);
        //    hero.addItem(item);
        //    Assert.AreSame(item, hero.Inventory[item.slot]);
        //}

        //[TestMethod]
        //public void characterAddedBetterItem()
        //{
        //    Character hero = new Character();
        //    Item sword = new Item("sword", "it's a sword", 1, "sword", 1);
        //    Item betterSword = new Item("better sword", "it's a better sword", 2, "sword", 1);
        //    hero.addItem(sword);
        //    hero.addItem(betterSword);
        //    Assert.AreSame(betterSword, hero.Inventory[1]);
        //}

    }
}

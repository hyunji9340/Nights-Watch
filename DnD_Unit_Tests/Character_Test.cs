using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroupProject_DD;
using GroupProject_DD.Models;

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
            hero.takeDamage(2500);
            Assert.IsTrue(hero.isDead());
        }

        [TestMethod]
        public void CharacterResetStatus()
        {
            Character hero = new Character();
            hero.takeDamage(25);
            hero.ResetStatus();
            Assert.IsTrue(hero.curHealth == hero.Health);
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
            int health_placeholder = hero.Health;
            hero.takeDamage(10);
            Assert.AreNotSame(hero.Health, health_placeholder);
        }

        [TestMethod]
        public void characterLevelUp()
        {
            Character hero = new Character();
            hero.levelUp();
            Assert.IsTrue(hero.Level > 1);
        }

        [TestMethod]
        public void characterEvaluatesNewItemAndEquips()
        {
            Character hero = new Character();

            Item item1 = new Item("", "Helmet", "It's story", 3, Bodypart.Head, "STRENGTH", 20);
            Item item2 = new Item("", "Helmet", "It's story", 7, Bodypart.Head, "STRENGTH", 20);
            hero.Inventory.Remove(Bodypart.Head);
            hero.equipItem(item1);
            Assert.IsTrue(hero.evaluateNewItem(item2));
        }

        [TestMethod]
        public void characterDiscardsItem()
        {
            Character hero = new Character();
            int invent_count = hero.Inventory.Count;
            Item onHead = new Item(hero.Inventory[Bodypart.Head]);
            //hero.Inventory.TryGetValue(Bodypart.Head, out onHead);
            hero.discardItem(onHead);
            Assert.IsTrue(invent_count > hero.Inventory.Count);
        }

        [TestMethod]
        public void maxHealthResetWhenDowngradingArmor()
        {
            Character hero = new Character();
            hero.Health = 10;
            hero.curHealth = 50;
            hero.verifyHealth();
            Assert.IsTrue(hero.curHealth == hero.Health);
        }

        [TestMethod]
        public void characterAttributesAddedWithItem()
        {
            Character hero = new Character();
            hero.Inventory.Remove(Bodypart.AttkArm);
            Item item = new Item("", "Helmet", "It's story", 7, Bodypart.AttkArm, "STRENGTH", 20);
            int heroSTRbeforeItem = hero.Strength;
            hero.equipItem(item);
            Assert.IsTrue(heroSTRbeforeItem < hero.Strength);
        }

        [TestMethod]
        public void characterAttributesRemovedWithItem()
        {
            Character hero = new Character();
            Item item = new Item("", "Helmet", "It's story", 7, Bodypart.AttkArm, "STRENGTH", 20);
            hero.Inventory.Remove(Bodypart.AttkArm);
            hero.equipItem(item);
            int heroSTRafterItem = hero.Strength;
            hero.discardItem(item);
            Assert.IsTrue(heroSTRafterItem > hero.Strength);
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


    }
}

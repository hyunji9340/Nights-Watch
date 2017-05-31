using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroupProject_DD;
using GroupProject_DD.Models;

namespace DnD_Unit_Tests
{
    [TestClass]
    public class ItemTest
    {
        Item item;

        [TestMethod]
        public void ItemUsage()
        {
            Item item = new Item("", "Sword", "It's Sharp", 7, Bodypart.Feet, "STRENGTH", 20);
            int durability = item.Usage;
            item.Usage--;
            Assert.IsTrue(durability > item.Usage);
        }

        [TestMethod]
        public void ItemBodypartHEAD()
        {
            Item item = new Item("", "Helmet", "It's story", 7, Bodypart.Head, "STRENGTH", 20);
            Assert.IsTrue(item.BodyPart == Bodypart.Head);
        }

        [TestMethod]
        public void ItemBodypartAttackingARM()
        {
            Item item = new Item("", "Sword", "It's Sharp", 7, Bodypart.AttkArm, "STRENGTH", 20);
            Assert.IsTrue(item.BodyPart == Bodypart.AttkArm);
        }

        [TestMethod]
        public void ItemBodypartDefendingARM()
        {
            Item item = new Item("", "Sheild", "It's heavy", 7, Bodypart.DefArm, "STRENGTH", 20);
            Assert.IsTrue(item.BodyPart == Bodypart.DefArm);
        }

        [TestMethod]
        public void ItemBodypartTorso()
        {
            Item item = new Item("", "Armor", "It's chain", 7, Bodypart.Torso, "STRENGTH", 20);
            Assert.IsTrue(item.BodyPart == Bodypart.Torso);
        }

        [TestMethod]
        public void ItemBodypartFeet()
        {
            Item item = new Item("", "Boots", "It's hurt", 7, Bodypart.Feet, "STRENGTH", 20);
            Assert.IsTrue(item.BodyPart == Bodypart.Feet);
        }
    }
}

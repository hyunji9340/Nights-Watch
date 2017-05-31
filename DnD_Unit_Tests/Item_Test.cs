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
        public void Item_Argument_Constructor()
        {
            Item item = new Item("", "Sword", "It's Sharp", 7, Bodypart.Feet, "STRENGTH", 20);
            Assert.AreSame(item, item);
        }

        [TestMethod]
        public void ItemBodypartHEAD()
        {
            Item item = new Item("", "Sword", "It's Sharp", 7, Bodypart.Head, "STRENGTH", 20);
            Assert.IsTrue(item.BodyPart == Bodypart.Head);
        }

        [TestMethod]
        public void ItemBodypartAttackingARM()
        {
            Item item = new Item("", "Sword", "It's Sharp", 7, Bodypart.AttkArm, "STRENGTH", 20);
            int attributeValue = 0;
            item.Attributes.TryGetValue("str", out attributeValue);
            Assert.IsTrue(0 < attributeValue);
        }

        [TestMethod]
        public void ItemBodypartDefendingARM()
        {
            Item item = new Item("", "Sword", "It's Sharp", 7, Bodypart.DefArm, "STRENGTH", 20);
            int attributeValue = 0;
            item.Attributes.TryGetValue("def", out attributeValue);
            Assert.IsTrue(0 < attributeValue);
        }

        [TestMethod]
        public void ItemBodypartTorso()
        {
            Item item = new Item("", "Sword", "It's Sharp", 7, Bodypart.Torso, "STRENGTH", 20);
            int attributeValue = 0;
            item.Attributes.TryGetValue("HP", out attributeValue);
            Assert.IsTrue(0 < attributeValue);
        }

        [TestMethod]
        public void ItemBodypartFeet()
        {
            Item item = new Item("", "Sword", "It's Sharp", 7, Bodypart.Feet, "STRENGTH", 20);
            int attributeValue = 0;
            item.Attributes.TryGetValue("agl", out attributeValue);
            Assert.IsTrue(0 < attributeValue);
        }
    }
}

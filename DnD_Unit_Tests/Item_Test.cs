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
            Item item = new Item("Winged Boots", "It's so light", 7, Bodypart.Feet);
            Assert.AreSame(item, item);
        }

        [TestMethod]
        public void ItemBodypartHEAD()
        {
            Item item = new Item("Winged Boots", "It's so light", 7, Bodypart.Head);
            int attributeValue = 0;
            item.Attributes.TryGetValue("dex", out attributeValue);
            Assert.IsTrue(0 < attributeValue);
        }

        [TestMethod]
        public void ItemBodypartAttackingARM()
        {
            Item item = new Item("Winged Boots", "It's so light", 7, Bodypart.AttkArm);
            int attributeValue = 0;
            item.Attributes.TryGetValue("str", out attributeValue);
            Assert.IsTrue(0 < attributeValue);
        }

        [TestMethod]
        public void ItemBodypartDefendingARM()
        {
            Item item = new Item("Winged Boots", "It's so light", 7, Bodypart.DefArm);
            int attributeValue = 0;
            item.Attributes.TryGetValue("def", out attributeValue);
            Assert.IsTrue(0 < attributeValue);
        }

        [TestMethod]
        public void ItemBodypartTorso()
        {
            Item item = new Item("Winged Boots", "It's so light", 7, Bodypart.Torso);
            int attributeValue = 0;
            item.Attributes.TryGetValue("HP", out attributeValue);
            Assert.IsTrue(0 < attributeValue);
        }

        [TestMethod]
        public void ItemBodypartFeet()
        {
            Item item = new Item("Winged Boots", "It's so light", 7, Bodypart.Feet);
            int attributeValue = 0;
            item.Attributes.TryGetValue("agl", out attributeValue);
            Assert.IsTrue(0 < attributeValue);
        }
    }
}

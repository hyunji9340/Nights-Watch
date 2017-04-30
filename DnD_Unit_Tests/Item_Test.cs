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
        public void Item_Default_Constructor()
        {
            item = new Item();
        }

        [TestMethod]
        public void Item_Argument_Constructor()
        {
            Item item = new Item("Winged Boots", "It's so light", 7, Bodypart.Feet);
            Assert.AreSame(item, item);
        }
    }
}

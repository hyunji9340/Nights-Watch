using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroupProject_DD;
using GroupProject_DD.Models;

namespace DnD_Unit_Tests
{
    //[TestClass]
    //public class Monster_Test
    //{
    //    [TestMethod]
    //    public void isMonsterDead()
    //    {
    //        Monster monster = new Monster();
    //        monster.takeDamage(25);
    //        Assert.IsTrue(monster.isDead());
    //    }

    //    [TestMethod]
    //    public void MonsterAliveDeadAfterTakingDamage()
    //    {
    //        Monster monster = new Monster();
    //        monster.takeDamage(5);
    //        Assert.IsFalse(monster.isDead());
    //    }

    //    [TestMethod]
    //    public void monsterTakesDamageAndDies()
    //    {
    //        Monster monster = new Monster();
    //        int health_placeholder = monster.curHealth;
    //        monster.takeDamage(10);
    //        Assert.AreNotSame(monster.curHealth, health_placeholder);
    //    }

    //    [TestMethod]
    //    public void isMonsterAlive()
    //    {
    //        Monster monster = new Monster();
    //        Assert.IsFalse(monster.isDead());
    //    }

    //    [TestMethod]
    //    public void MonsterAddsItem()
    //    {
    //        Monster monster = new Monster();
    //        Item item = new Item("Sword", "Hand", 1, Bodypart.AttkArm);
    //        monster.addItem(item);
    //        Assert.IsTrue(monster.hasItem());
    //    }
    //    [TestMethod]
    //    public void MonsterDoesntHaveItem()
    //    {
    //        Monster monster = new Monster();
    //        Assert.IsFalse(monster.hasItem());
    //    }

    //    [TestMethod]
    //    public void MonsterDropItem()
    //    {
    //        Monster monster = new Monster();
    //        Item item = new Item("Sword", "Hand", 1, Bodypart.AttkArm);
    //        monster.addItem(item);
    //        Assert.AreSame(item, monster.discardItem());
    //    }

    //    [TestMethod]
    //    public void MonsterAttackAlgorithm()
    //    {
    //        Monster monster = new Monster();
    //        Assert.IsTrue(monster.Level + monster.Strength == monster.Attack());
    //    }
    //}
}

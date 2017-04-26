using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroupProject_DD;

namespace DnD_Unit_Tests
{
    [TestClass]
    public class Monster_Test
    {
        [TestMethod]
        public void isMonsterDead()
        {
            Monster monster = new Monster();
            monster.takeDamage(25);
            Assert.IsTrue(monster.isDead());
        }

        [TestMethod]
        public void MonsterAliveDeadAfterTakingDamage()
        {
            Monster monster = new Monster();
            monster.takeDamage(10);
            Assert.IsFalse(monster.isDead());
        }

        [TestMethod]
        public void monsterTakesDamageAndDies()
        {
            Monster monster = new Monster();
            int health_placeholder = monster.Health;
            monster.takeDamage(10);
            Assert.AreNotSame(monster.Health, health_placeholder);
        }

        [TestMethod]
        public void isMonsterAlive()
        {
            Monster monster = new Monster();
            Assert.IsFalse(monster.isDead());
        }
    }
}

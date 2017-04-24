using System;
namespace XamarinProject
{
    public class Monster
    {
        //fields
        public string Name { get; set; }
        public int Rating { get; set; }
        public int Health { get; set; }
        public int xpValue { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Agility { get; set; }
        public int Speed { get; set; }
        public int Item { get; set; }
        // constructor
        public Monster()
        {
            Name = "";
            Rating = 1;
            Health = 20;
            xpValue = 0;
            Strength = 1;
            Dexterity = 1;
            Agility = 1;
            Speed = 1;
        }

        public Monster(string name)
        {
            Name = name;
            Rating = 1;
            Health = 20;
            xpValue = 0;
            Strength = 1;
            Dexterity = 1;
            Agility = 1;
            Speed = 1;
        }
        public Monster(Monster monster)
        {
            Name = monster.Name;
            Rating = monster.Rating;
            Health = monster.Health;
            xpValue = monster.xpValue;
            Strength = monster.Strength;
            Dexterity = monster.Dexterity;
            Agility = monster.Agility;
            Speed = monster.Speed;
        }

        public void setMonsterLevel(int level)
        {
            Rating = 1 * level;
            Health = 20 * level;
            xpValue = 10 * level;
            Strength = 1 * level;
            Dexterity = 1 * level;
            Agility = 1 * level;
            Speed = 1 * level;
        }

        public bool isDead()
        {
            return (Health <= 0);
        }

        public void takeDamage(int damage)
        {
            Health -= damage;
        }

        public int Attack()
        {
            return Strength + Rating;
        }

    }
}
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

        public bool isDead()
        {
            return (Health <= 0);
        }

        public void takeDamage(int damage)
        {
            Health -= damage;
        }

    }
}
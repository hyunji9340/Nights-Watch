using System;
namespace XamarinProject
{
    public class Monster
    {
        //fields
        public string name { get; set; }
        public int rating { get; set; }
        public int health { get; set; }
        public int xpValue { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Agility { get; set; }
        public int Speed { get; set; }

        // constructor
        public Monster()
        {
            name = "";
            rating = 1;
            health = 20;
            xpValue = 0;
            Strength = 1;
            Dexterity = 1;
            Agility = 1;
            Speed = 1;
        }

        bool isDead() { return (health <= 0); }

    }
}

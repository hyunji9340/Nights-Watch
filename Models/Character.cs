using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    public class Character
    {
        string Name { get; set; }
        int Level { get; set; }
        int maxHealth { get; set; }
        int curHealth { get; set; }
        int Experience { get; set; }
        int Strength { get; set; }
        int Dexterity { get; set; }
        int Agility { get; set; }
        int Speed { get; set; }
        Item[] Inventory { get; set; }

        public Character()
        {
            Name = "";
            Level = 1;
            maxHealth = 20;
            curHealth = 20;
            Experience = 0;
            Strength = 1;
            Dexterity = 1;
            Agility = 1;
            Speed = 1;
        }

        bool isDead() { return (curHealth <= 0); }
        void levelUp()
        {
            maxHealth += 5;
            curHealth = maxHealth;
            Strength++;
            Dexterity++;
            Agility++;
            Speed++;
        }


        void addExperience(int gain)
        {
            Experience += gain;
            if (Experience > 10) //10 is used in this case, will be replaced by global xp thresholds
            {
                levelUp();
                Experience -= 10;
            }
        }


        void addItem(Item item)
        {
            item = new XamarinProject.Item();
            item.Id = "Sword/RightHand";
            item.Text = "The Shiny Blade of Dragon Slaying";
            item.Description = "This sword slays dragons yo.";

            Inventory[0] = item;
        }
    }

}

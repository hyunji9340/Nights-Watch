using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_DD.Models
{
    public interface ICreature
    {
        int takeDamage(int damage);
        int Attack();
        int Speed { get; set; }
        string Name { get; set; }
        int Dexterity { get; set; }
        int Experience { get; set; }
        int Agility { get; set; }
        int Level { get; set; }
        bool isDead();
    }
}

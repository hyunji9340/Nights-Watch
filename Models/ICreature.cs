using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_DD.Models
{
    public interface ICreature
    {
        int Id();
        string Name();
        int Level();
        int Health();
        int Strength();
        int Dexterity();
        int xpValue();
        int Speed();
        void takeDamage(int damage);
        int Attack();
    }
}

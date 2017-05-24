using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_DD.Models
{
    class BattleEffects
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Tier { get; set; }
        public string Target { get; set; }
        public string AttribMod { get; set; }


        public BattleEffects(string name, string desrc, int tier, string target, string attribute)
        {
            Name = name;
            Description = desrc;
            Tier = tier;
            Target = target;
            AttribMod = attribute;

        }

        public string UseEffect(List<Character> CharList, List<Monster> MonsterList)
        {
            if(this.Target == "ALL")
            {
                foreach(var character in CharList)
                {
                    if (this.AttribMod == "SPEED")
                        character.Speed += this.Tier;
                    if (this.AttribMod == "HP")
                        character.curHealth += this.Tier;
                    if (this.AttribMod == "STRENGTH")
                        character.Strength += this.Tier;
                    if (this.AttribMod == "DEFENSE")
                        character.Defense += this.Tier;
                }

                foreach (var monster in MonsterList)
                {
                    if (this.AttribMod == "SPEED")
                        monster.Speed += this.Tier;
                    if (this.AttribMod == "HP")
                        monster.curHealth += this.Tier;
                    if (this.AttribMod == "STRENGTH")
                        monster.Strength += this.Tier;
                    if (this.AttribMod == "DEFENSE")
                        monster.Defense += this.Tier;
                }

                return this.Description;

            }else if(this.Target == "CHARACTERALL")
            {
                foreach (var character in CharList)
                {
                    if (this.AttribMod == "SPEED")
                        character.Speed += this.Tier;
                    if (this.AttribMod == "HP")
                        character.curHealth += this.Tier;
                    if (this.AttribMod == "STRENGTH")
                        character.Strength += this.Tier;
                    if (this.AttribMod == "DEFENSE")
                        character.Defense += this.Tier;
                }

                return this.Description;

            }else if( this.Target == "MONSTERALL")
            {
                foreach (var monster in MonsterList)
                {
                    if (this.AttribMod == "SPEED")
                        monster.Speed += this.Tier;
                    if (this.AttribMod == "HP")
                        monster.curHealth += this.Tier;
                    if (this.AttribMod == "STRENGTH")
                        monster.Strength += this.Tier;
                    if (this.AttribMod == "DEFENSE")
                        monster.Defense += this.Tier;
                }

                return this.Description;
            }
            return this.Description;

        }
    }
}

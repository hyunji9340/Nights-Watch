using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    class Engine
    {
        /***********************resource declarations***************************
         public characater list
         public monster list
         public character_dead list
         public item list
         flag manual_mode <- will step through engine
        */

        /**********************Game Start**************************************
         public Engine() 
            //instantiate all lists
            //pass characters into lists
            //initialize score to zero
            //start(0)
         
         public start(int dungeon level)
         {
            if (character list isn't empty)
                explore_dungeon(dungeon_level);
           else
                endgame() <- goes to score screen
         }
         
         public eplore_dungeon(dungeon_level)
         {
         generate a bunch of dungeon_level monster and put them in the list
         while (monster list is isn't empty and character list isn't empty)
                battle(characterlist top, monsterlist top)
                if characterlist top is dead
                    add character to dead list
                else
                    put character at back of the list
         start(dungeon_level++)
         }
         
         public battle(character, monster)
         {
            monster.health -= charater.strength
            if (monster isn't dead)
                character.health -= monster.health
            else
                character.addexp(monster.exp)
                character.addItem(monster.item)
         }
         
         
         
         */

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_DD.Models
{
    public class serverEvents
    {
        public string msg { get; set; }
        public int errorCode { get; set; }


        //public ObservableCollection<Item> data { get; set; }
        public List<BattleEffects> data { get; set; }


        public serverEvents()
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GroupProject_DD.Models;

namespace GroupProject_DD
{
    public class ServerItems
    {
        // fields

        public string msg { get; set; }
        public int errorCode { get; set; }


        //public ObservableCollection<Item> data { get; set; }
        public List<Item> data { get; set; }


		public ServerItems()
		{

		}


	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GroupProject_DD
{
	public class CharacterDetailViewModel
	{
		public Character Character { get; set; }
        public Dictionary<string, Item> Inventory { get; set; }
        public CharacterDetailViewModel()
		{

		}
		public CharacterDetailViewModel(Character character = null) 
		{
			Character = character;
            Inventory = Character.Inventory;
		}
	}
}


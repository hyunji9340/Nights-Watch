using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace GroupProject_DD
{
	public class MonsterDetailViewModel
	{
		public Monster Monster { get; set; }

		public MonsterDetailViewModel()
		{
		}

		public MonsterDetailViewModel(Monster monster = null)
		{
			Monster = monster;
		}
	}
}

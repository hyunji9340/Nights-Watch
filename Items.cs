using System;
namespace GroupProject
{
	public class Items
	{
		// fields
		public String name { get; set; }
		public String description { get; set; }
		public int rating { get; set; }
		public String type { get; set; }
		public int slot { get; set; }

		// default constructor that doesn't take any arguments
		public Items() { }

		public Items(String name, String description, int rating, String type, int slot)
		{

			this.name = name;
			this.description = description;
			this.rating = rating;
			this.type = type;
			this.slot = slot;
		}
	}
}

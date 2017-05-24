using System;
using System.Collections.Generic;

namespace GroupProject_DD
{
	public class ServerItems
	{
		// fields

		public string msg { get; set; }
		public int errorCode { get; set; }
		public List<Item> data { get; set; }

		public ServerItems()
		{
		}
	}
}

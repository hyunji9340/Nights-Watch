using System;
using SQLite;
using GroupProject_DD.Droid;
using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(SQLiteDb))]

namespace GroupProject_DD.Droid
{
	public class SQLiteDb : ISQLiteDb
	{
		public SQLiteConnection DbConnection()
		{
			var dbName = "ItemsDb.db4";
			var path = Path.Combine(System.Environment.
			  GetFolderPath(System.Environment.
			  SpecialFolder.Personal), dbName);
			return new SQLiteConnection(path);
		}
	}
}

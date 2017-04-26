using System;
using GroupProject_DD.iOS;
using SQLite;
using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(SQLiteDb))]

namespace GroupProject_DD.iOS
{
	public class SQLiteDb : ISQLiteDb
	{
		public SQLiteConnection DbConnection()
		{
			var dbName = "ItemsDb.db4";
			string personalFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string libraryFolder = Path.Combine(personalFolder, "..", "Library");
			var path = Path.Combine(libraryFolder, dbName);
			return new SQLiteConnection(path);
		}
	}
}




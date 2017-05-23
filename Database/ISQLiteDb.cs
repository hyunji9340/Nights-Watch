using System;
namespace GroupProject_DD
{
	public interface ISQLiteDb
	{
		SQLite.SQLiteConnection DbConnection();
	}
}



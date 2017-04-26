using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace GroupProject_DD
{
	public class MonsterController
	{
		private SQLiteConnection database;
		private static object collisionLock = new object();
		public ObservableCollection<Monster> Monsters { get; set; }

		public MonsterController()
		{
			database = DependencyService.Get<ISQLiteDb>().DbConnection();
			database.CreateTable<Monster>();
			this.Monsters = new ObservableCollection<Monster>(database.Table<Monster>());
			// If the table is empty, initialize the collection
			if (!database.Table<Monster>().Any())
			{
				AddNewMonsters();
			}
		}

		public void AddNewMonsters() 
		{
			Monster monster1 = new Monster();
			monster1.Name = "Troll";
			Monster monster2 = new Monster();
			monster2.Name = "Hobbit";
			this.Monsters.Add(monster1);
			this.Monsters.Add(monster2);
		}

		// get all items from Character table
		public IEnumerable<Monster> GetAllMonsters()
		{
			lock (collisionLock)
			{
				return database.Query<Monster>("SELECT * FROM Monsters").AsEnumerable();
			}
		}

		// add new Character to the database
		public int SaveMonster(Monster monsterInstance)
		{

			lock (collisionLock)
			{
				if (monsterInstance.Id != 0)
				{
					database.Update(monsterInstance);
					return monsterInstance.Id;
				}
				else
				{
					database.Insert(monsterInstance);
					return monsterInstance.Id;
				}
			}
		}

		// delete a character 
		public int DeleteMonster(Monster monsterInstance)
		{
			var id = monsterInstance.Id;
			if (id != 0)
			{
				lock (collisionLock)
				{
					database.Delete<Monster>(id);
				}
			}
			this.Monsters.Remove(monsterInstance);
			return id;
		}
	}
}

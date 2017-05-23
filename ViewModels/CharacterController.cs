using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using GroupProject_DD.Models;

namespace GroupProject_DD
{
	public class CharacterController
	{
		private SQLiteConnection database;
		private static object collisionLock = new object();
		public ObservableCollection<ICreature> Characters { get; set; }

		public CharacterController()
		{
			database = DependencyService.Get<ISQLiteDb>().DbConnection();
			database.CreateTable<Character>();
			this.Characters = new ObservableCollection<ICreature>(database.Table<Character>());
			// If the table is empty, initialize the collection
			if (!database.Table<Character>().Any())
			{
				AddNewCharacters();
			}
		}

		public void AddNewCharacters() 
		{
			Character character1 = new Character("Andrew");

			Character character2 = new Character("Simon");

			this.Characters.Add(character1);
			this.Characters.Add(character2);
		}

		// get all items from Character table
		public IEnumerable<ICreature> GetAllItems()
		{
			lock (collisionLock)
			{
				return database.Query<Character>("SELECT * FROM Characters").AsEnumerable();
			}
		}

		// add new Character to the database
		public int SaveCharacter(Character characterInstance)
		{

			lock (collisionLock)
			{
				if (characterInstance.Id != 0)
				{
					database.Update(characterInstance);
					return characterInstance.Id;
				}
				else
				{
					database.Insert(characterInstance);
					return characterInstance.Id;
				}
			}
		}

		// delete a character 
		public int DeleteCharacter(Character characterInstance)
		{
			var id = characterInstance.Id;
			if (id != 0)
			{
				lock (collisionLock)
				{
					database.Delete<Character>(id);
				}
			}
			this.Characters.Remove(characterInstance);
			return id;
		}
	}
}

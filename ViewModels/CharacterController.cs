﻿using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace GroupProject_DD
{
	public class CharacterController
	{
		private SQLiteConnection database;
		private static object collisionLock = new object();
		public ObservableCollection<Character> Characters { get; set; }

		public CharacterController()
		{
			database = DependencyService.Get<ISQLiteDb>().DbConnection();
			database.CreateTable<Character>();
			this.Characters = new ObservableCollection<Character>(database.Table<Character>());
			// If the table is empty, initialize the collection
			if (!database.Table<Character>().Any())
			{
				AddNewCharacters();
			}
		}

		public void AddNewCharacters() 
		{
			Character character1 = new Character();
			character1.Name = "character1";
			character1.curHealth = 10;
			character1.Defense = 10;
			character1.Dexterity = 10;
			character1.Experience = 0;
			character1.Health = 100;
			character1.Level = 1;
			character1.monstersKilled = 0;

			Character character2 = new Character();
			character2.Name = "character2";
			character2.curHealth = 10;
			character2.Defense = 10;
			character2.Dexterity = 10;
			character2.Experience = 0;
			character2.Health = 100;
			character2.Level = 1;
			character2.monstersKilled = 0;

			this.Characters.Add(character1);
			this.Characters.Add(character2);
		}

		// get all items from Character table
		public IEnumerable<Character> GetAllItems()
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
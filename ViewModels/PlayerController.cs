using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace GroupProject_DD
{
    public class PlayerController
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<Player> Players { get; set; }

        public PlayerController()
        {
            database = DependencyService.Get<ISQLiteDb>().DbConnection();
            database.CreateTable<Player>();
            this.Players = new ObservableCollection<Player>(database.Table<Player>());
        }

        // get all items from Player table
        public IEnumerable<Player> GetAllPlayers()
        {
            lock (collisionLock)
            {
                return database.Query<Player>("SELECT * FROM Players").AsEnumerable();
            }
        }

        // delete all players
        public void DeleteAllPlayers()
        {
            lock (collisionLock)
            {
                database.Query<Player>("DELETE FROM Players");
            }
        }

        // add new Player to the database
        public int SavePlayer(Player playerInstance)
        {

            lock (collisionLock)
            {
                if (playerInstance.Id != 0)
                {
                    database.Update(playerInstance);
                    return playerInstance.Id;
                }
                else
                {
                    database.Insert(playerInstance);
                    return playerInstance.Id;
                }
            }
        }

        // delete a Player
        public int DeletePlayer(Player playerInstance)
        {
            var id = playerInstance.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Player>(id);
                }
            }
            this.Players.Remove(playerInstance);
            return id;
        }

        public IEnumerable<Player> OrderedPlayers()
        {
            lock (collisionLock)
            {
                return database.Query<Player>("SELECT * FROM Players ORDER BY TotalScore DESC").AsEnumerable();
            }
        }
    }
}

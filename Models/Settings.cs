using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_DD.Models
{
    public class Settings : INotifyPropertyChanged
    {
        private bool serverItems;
        public bool ServerItems
        {
            get { return serverItems; }
            set
            {

                serverItems = value;
                OnPropertyChanged(nameof(ServerItems));
            }
        }
        private bool itemsRandom;
        public bool ItemsRandom
        {
            get { return itemsRandom; }
            set
            {
                itemsRandom = value;
                OnPropertyChanged(nameof(ItemsRandom));
            }
        }
        private bool superItems;
        public bool SuperItems
        {
            get { return superItems; }
            set
            {
                superItems = value;
                OnPropertyChanged(nameof(SuperItems));
            }
        }
        private bool debugSettings;
        public bool DebugSettings
        {
            get { return debugSettings; }
            set
            {
                debugSettings = value;
                OnPropertyChanged(nameof(DebugSettings));
            }
        }
        private bool everyCriticalHit;
        public bool EveryCriticalHit
        {
            get { return everyCriticalHit; }
            set
            {
                everyCriticalHit = value;
                OnPropertyChanged(nameof(EveryCriticalHit));
            }
        }
        private bool everyCriticalMiss;
        public bool EveryCriticalMiss
        {
            get { return everyCriticalMiss; }
            set
            {
                everyCriticalMiss = value;
                OnPropertyChanged(nameof(EveryCriticalMiss));
            }
        }
        private bool itemUsage;
        public bool ItemUsage
        {
            get { return itemUsage; }
            set
            {
                itemUsage = value;
                OnPropertyChanged(nameof(ItemUsage));
            }
        }
        private bool magicUsage;
        public bool MagicUsage
        {
            get { return magicUsage; }
            set
            {
                magicUsage = value;
                OnPropertyChanged(nameof(MagicUsage));
            }
        }
        private bool healing;
        public bool Healing
        {
            get { return healing; }
            set
            {
                healing = value;
                OnPropertyChanged(nameof(Healing));
            }
        }
        private bool battleEvents;
        public bool BattleEvents
        {
            get { return battleEvents; }
            set
            {
                battleEvents = value;
                OnPropertyChanged(nameof(BattleEvents));
            }
        }

        public Settings()
        {
            ServerItems = true;
            ItemsRandom = false;
            SuperItems = false;
            DebugSettings = false;
            EveryCriticalHit = false;
            EveryCriticalMiss = false;
            ItemUsage = false;
            MagicUsage = false;
            Healing = false;
            BattleEvents = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

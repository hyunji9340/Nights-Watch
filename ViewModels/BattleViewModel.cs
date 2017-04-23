using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamarinProject.ViewModels
{
    public class BattleViewModel : INotifyPropertyChanged
    {
        Character char1;
        Character char2;
        Character char3;
        Character char4;
        List<Item> item_dictionary;

        public string action;

        public BattleViewModel()
        {
            char1 = new Character();
            char2 = new Character();
            char3 = new Character();
            char4 = new Character();
            item_dictionary = new List<Item>();
            action = "Game starting...";
        }

        public string Action
        {
            get
            {
                return action;
            }
            set
            {
                if (action != value)
                {
                    action = value;
                    OnPropertyChanged("Action");
                }
            }
        }

        public void UpdateAction(ref int counter)
        {
            action = counter.ToString() + "\n";
            OnPropertyChanged("Action");
            counter++;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}

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
        public ObservableRangeCollection<String> actions;
        public string action;

        public BattleViewModel()
        {
            char1 = new Character();
            char2 = new Character();
            char3 = new Character();
            char4 = new Character();
            item_dictionary = new List<Item>();
            action = "Game starting...";
            actions = new ObservableRangeCollection<string>();
        }

        public void Enque(string move)
        {
            if (actions.Count >= 11)
                actions.RemoveAt(actions.Count - 1);
            actions.Insert(0, move);
        }

        public ObservableRangeCollection<string> Actions
        {
            get
            {
                return actions;
            }
            set
            {
                    actions = value;
                    OnPropertyChanged("Actions");
            }
        }


        public void UpdateAction(ref int counter)
        {
            Enque(counter.ToString());
            OnPropertyChanged("Actions");
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

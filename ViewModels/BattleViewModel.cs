using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamarinProject.ViewModels
{
    public class BattleViewModel
    {
        Character char1;
        Character char2;
        Character char3;
        Character char4;
        List<Item> item_dictionary;

        string Action;

        public BattleViewModel()
        {
            char1 = new Character();
            char2 = new Character();
            char3 = new Character();
            char4 = new Character();
            item_dictionary = new List<Item>();
            Action = "Game starting...";
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

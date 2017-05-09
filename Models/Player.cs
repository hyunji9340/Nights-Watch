using System;
using SQLite;
using System.ComponentModel;

namespace GroupProject_DD
{
    [Table("Players")]
    public class Player : INotifyPropertyChanged
    {
        public Player()
        {
        }

        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _id; }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));

            }
        }

        private string _Name;
        [NotNull]
        public string Name
        {
            get { return _Name; }
            set
            {
                this._Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _TotalScore;
        public int TotalScore
        {
            get { return _TotalScore; }
            set
            {
                this._TotalScore = value;
                OnPropertyChanged(nameof(TotalScore));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

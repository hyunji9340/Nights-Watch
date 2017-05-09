using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GroupProject_DD
{
    public partial class AddMonsterPage : ContentPage
    {
        public Monster monster { get; set; }
        private MonsterController monsterController;

        public AddMonsterPage(MonsterController monsterController)
        {
            InitializeComponent();
            this.monsterController = monsterController;
            monster = new Monster();
            BindingContext = this;
        }

        public AddMonsterPage(MonsterController monsterController, Monster monster)
        {
            InitializeComponent();
            this.monsterController = monsterController;
            this.monster = monster;
            BindingContext = this;
        }

        // stores the item into the database by calling dataAccess 
        async void SaveClicked(object sender, EventArgs e)
        {
            this.monsterController.SaveMonster(monster);
            await Navigation.PopAsync();
        }
    }
}

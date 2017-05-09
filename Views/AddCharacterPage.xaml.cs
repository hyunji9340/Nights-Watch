using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GroupProject_DD
{
    public partial class AddCharacterPage : ContentPage
    {
        public Character Character { get; set; }
        private CharacterController characterController;

        public AddCharacterPage(CharacterController characterController)
        {
            InitializeComponent();
            this.characterController = characterController;
            Character = new Character();
            BindingContext = this;
        }

        public AddCharacterPage(CharacterController characterController, Character character)
        {
            InitializeComponent();
            this.characterController = characterController;
            this.Character = character;
            this.Character.curHealth = this.Character.Health;
            this.Character.Name = character.Name;
            BindingContext = this;
        }

        // stores the item into the database by calling dataAccess 
        async void SaveClicked(object sender, EventArgs e)
        {
            this.characterController.SaveCharacter(Character);
            await Navigation.PopAsync();
        }
    }
}
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinProject
{
	public partial class CharactersPage : ContentPage
	{
		private List<Character> characterList;

		public CharactersPage()
		{
			InitializeComponent();
			PopulateCharacters();
			CharacterListView.ItemsSource = characterList;
		}

		// temporary method
		public void PopulateCharacters() 
		{
			characterList = new List<Character>();
			Character character1 = new Character();
			character1.Name = "Druid";
			Character character2 = new Character();
			character2.Name = "Elminster";
			Character character3 = new Character();
			character3.Name = "Elf";
			Character character4 = new Character();
			character4.Name = "Tiamat";
			Character character5 = new Character();
			character5.Name = "luz";
			characterList.Add(character1);
			characterList.Add(character2);
			characterList.Add(character3);
			characterList.Add(character4);
			characterList.Add(character5);
		}


		async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var character = args.SelectedItem as Character;

			if (character == null)
				return;

			// this leads to character information page // character detail view modeal
			//await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(character)));

			// Manually deselect item
			CharacterListView.SelectedItem = null;
		}
	}
}

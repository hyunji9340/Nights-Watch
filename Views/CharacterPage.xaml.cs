using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;

using Xamarin.Forms;

namespace GroupProject_DD
{
	public partial class CharacterPage : ContentPage
	{
		private CharacterController characterController;
		private CharacterDetailPage characterDetailPage;

		public CharacterPage()
		{
			InitializeComponent();
			this.characterController = new CharacterController();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			CharacterListView.ItemsSource = null;
			CharacterListView.ItemsSource = characterController.GetAllItems();
		}

		// This method directs a detail page for a specific item
		async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var character = args.SelectedItem as Character;

			if (character == null)
				return;

			this.characterDetailPage = new CharacterDetailPage(new CharacterDetailViewModel(character));
			characterDetailPage.setCharacterController(characterController);

			await Navigation.PushAsync(this.characterDetailPage);

			// Manually deselect item
			CharacterListView.SelectedItem = null;
		}

		// directs to monster page
		async void AddCharacterBtnClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddCharacterPage(characterController)); 
		}
	}
}

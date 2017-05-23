using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GroupProject_DD
{
	public partial class CharacterDetailPage : ContentPage
	{
		CharacterDetailViewModel characterViewModel;
		private CharacterController characterController;

		public CharacterDetailPage(CharacterDetailViewModel characterViewModel)
		{
			InitializeComponent();
			BindingContext = this.characterViewModel = characterViewModel;
		}

		public void setCharacterController(CharacterController characterController)
		{
			this.characterController = characterController;
		}

		// need to call update page
		async void UpdateBtnClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddCharacterPage(characterController, this.characterViewModel.Character));
		}

		async void DeleteBtnClicked(object sender, EventArgs e)
		{
			this.characterController.DeleteCharacter(this.characterViewModel.Character);
			await Navigation.PopToRootAsync();
		}
	}
}



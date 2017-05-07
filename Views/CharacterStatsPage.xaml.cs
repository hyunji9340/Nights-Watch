using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupProject_DD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterStatsPage : ContentPage
    {
        private CharacterDetailViewModel characterViewModel;
        private CharacterController characterController;

        public CharacterStatsPage(CharacterDetailViewModel characterViewModel)
        {
            InitializeComponent();
            BindingContext = this.characterViewModel = characterViewModel;
        }
        public void setCharacterController(CharacterController characterController)
        {
            this.characterController = characterController;
        }
    }
}

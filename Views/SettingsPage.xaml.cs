using GroupProject_DD.Models;
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
    public partial class SettingsPage : ContentPage
    {
        private Settings localSettings;
        public SettingsPage(Settings settings)
        {
            InitializeComponent();
            BindingContext = localSettings = settings;
        }
    }
}

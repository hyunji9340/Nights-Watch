using Xamarin.Forms;

namespace GroupProject_DD
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
            DependencyService.Get<IAudioPlayerService>().Play("prelude.mp3");
            //MainPage = new GroupProject_DDPage();
            MainPage = new NavigationPage(new StudentsInfoPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
            DependencyService.Get<IAudioPlayerService>().Pause();
            // Handle when your app sleeps
        }

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

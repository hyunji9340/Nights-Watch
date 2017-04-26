using Xamarin.Forms;

namespace GroupProject_DD
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			//MainPage = new GroupProject_DDPage();
			MainPage = new NavigationPage(new StudentsInfoPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

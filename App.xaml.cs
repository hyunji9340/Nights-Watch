using System.Collections.Generic;
using System;

using Xamarin.Forms;

namespace XamarinProject
{
	public partial class App : Application
	{
		public static bool UseMockDataStore = true;
		public static string BackendUrl = "https://localhost:5000";

		public static IDictionary<string, string> LoginParameters => null;

		public App()
		{
			InitializeComponent();

			if (UseMockDataStore)
				DependencyService.Register<MockDataStore>();
			else
				DependencyService.Register<CloudDataStore>();

			SetMainPage();
		}

		public static void SetMainPage()
		{
			if (!UseMockDataStore && !Settings.IsLoggedIn)
			{
				Current.MainPage = new NavigationPage(new LoginPage())
				{
					BarBackgroundColor = (Color)Current.Resources["Primary"],
					BarTextColor = Color.White
				};
			}
			else
			{
				GoToMainPage();
			}
		}

		public static void GoToMainPage()
		{
			Console.WriteLine("before calling new info class");
			Current.MainPage = new TabbedPage
			{
				Children = {
					new NavigationPage(new MyInfo()) // new ItemPage()
					{
						Title = "Browse" // Browse
					},
					new NavigationPage(new AboutPage())
					{
						Title = "About" // About
					},
				}
			};
		}
	}
}

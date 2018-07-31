using System;
using Xamarin.Forms;
using Verge.Mobile.Views;
using Xamarin.Forms.Xaml;
using Verge.Mobile.Services;
using System.Threading.Tasks;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Verge.Mobile
{
	public partial class App : Application
	{
		
		public App ()
		{
			InitializeComponent();

            if (Device.RuntimePlatform == Device.UWP)
            {
                InitNavigation();
            }
           
		}
        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override async void OnStart ()
		{
            base.OnStart();
            if (Device.RuntimePlatform != Device.UWP)
            {

                await InitNavigation();
            }// Handle when your app starts
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

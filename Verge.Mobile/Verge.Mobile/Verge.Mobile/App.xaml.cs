using System;
using Xamarin.Forms;
using Verge.Mobile.Views;
using Xamarin.Forms.Xaml;
using Verge.Mobile.Services;
using System.Threading.Tasks;
using System.Linq;

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
                LoadProperties();
                if (App.Current.MainPage == null) App.Current.MainPage = new RPCLoginPage();
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
                LoadProperties();
                await InitNavigation();
            }// Handle when your app starts
        }

		protected override async void OnSleep ()
		{
            base.OnSleep();
            SaveProperties();
            await SavePropertiesAsync();
        }

		protected override void OnResume ()
		{
            // Handle when your app resumes
            base.OnResume();
            LoadProperties();
        }
        public void LoadProperties()
        {
            Properties.Keys.ToList().ForEach((px) =>
            {
                Properties[px] = Newtonsoft.Json.JsonConvert.DeserializeObject<object>((string)Properties[px]);
            });

        }
        public void SaveProperties()
        {
            Properties.Keys.ToList().ForEach((px) =>
            {
                Properties[px] = Newtonsoft.Json.JsonConvert.SerializeObject(Properties[px]);
            });
        }
    }
}

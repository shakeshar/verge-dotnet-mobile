using Verge.Mobile.Services;
using Verge.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Verge.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        protected readonly INavigationService NavigationService;
        public MainPage()
        {
            InitializeComponent();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            if (Device.RuntimePlatform == Device.UWP)
            {
                Icon = "slideout.png";
                MasterBehavior = MasterBehavior.Popover;

            }
        }
        protected override void OnAppearing()
        {
            Load();
        }
        public void Load()
        {
            MasterPage.ListView.SelectedItem = MasterPage.Viewmodel.MenuItems[0];
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainViewPageMenuItem;
            if (item == null)
                return;
            NavigationService.NavigateToAsync(item.TargetType, false);
            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
        }
    }
}
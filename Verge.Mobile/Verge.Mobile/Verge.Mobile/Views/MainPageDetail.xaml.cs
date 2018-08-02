
using Verge.Mobile.Services;
using Verge.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Verge.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        private readonly INavigationService accountService;
        public MainPageDetail(INavigationService accountServiceSettings)
        {
            this.accountService = accountServiceSettings;
            InitializeComponent();
            accountService.NavigateToAsync<OverviewViewModel>();

        }
        public MainPageDetail()
        {            
            InitializeComponent();
        }
    }
}
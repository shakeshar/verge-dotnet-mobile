using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{
    public class StartViewModel : CollectionViewModel<object>
    {
        private bool canStart = true;
        private string username;
        private string password;
        public ICommand CreateReportCmd { get; private set; }
        public ICommand GuestCmd { get; private set; }
       

        public StartViewModel()
        {
            CreateReportCmd = new Command(async () => await Login(), () => canStart);
            GuestCmd = new Command(async () => await GuestLogin(), () => canStart);
          
        }
       
        private async Task GuestLogin()
        {
            canStart = false;
            IsBusy = true;
            ((Command)GuestCmd).ChangeCanExecute();
            //await NavigationService.NavigateToAsync<LoginGuestViewModel>();
            canStart = true;
            IsBusy = false;
            ((Command)GuestCmd).ChangeCanExecute();

        }
        private async Task Login()
        {
            canStart = false;
            IsBusy = true;
            ((Command)CreateReportCmd).ChangeCanExecute();
            //await NavigationService.NavigateToAsync<ServiceReportViewModel>();          
            canStart = true;
            IsBusy = false;
            ((Command)CreateReportCmd).ChangeCanExecute();
        }
    }
}

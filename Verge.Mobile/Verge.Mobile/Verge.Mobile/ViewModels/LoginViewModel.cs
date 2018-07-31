using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{
    public class LoginViewModel : CollectionViewModel<object>
    {
        private bool canStart = true;
        private string username;
        private string password;
        public ICommand LoginCmd { get; private set; }
        public ICommand GuestCmd { get; private set; }
        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }

        private const string LOGIN_CREDENTIALS_USERNAME = "LOGIN_CREDENTIALS_USERNAME";
        private const string LOGIN_CREDENTIALS_PASSWORD = "LOGIN_CREDENTIALS_PASSWORD";
        public LoginViewModel()
        {
            LoginCmd = new Command(async () => await Login(), () => canStart);
            if (Application.Current.Properties.ContainsKey(LOGIN_CREDENTIALS_USERNAME))
            {
                Username = (string)Application.Current.Properties[LOGIN_CREDENTIALS_USERNAME];
            }
            Username = "cPrJiV2qrzcg4FH4xv54F6Ko5Eq8QkbygLjUhbfHVEg9Zp2QpyzR"; //Private key for testnet account
        }

        private async Task Login()
        {
            canStart = false;
            IsBusy = true;
            ((Command)LoginCmd).ChangeCanExecute();
            //App.Account = new Domain.Strat(Username, NBitcoin.Network.TestNet);
            await NavigationService.NavigateToAsync<MainViewModel>();
            try
            {
                Application.Current.Properties[LOGIN_CREDENTIALS_PASSWORD] = Password;
                Application.Current.Properties[LOGIN_CREDENTIALS_USERNAME] = Username;
            }
            catch (Exception e)
            {
              await  NavigationService.Display(e.Message);
            }
            canStart = true;
            IsBusy = false;
            ((Command)LoginCmd).ChangeCanExecute();
        }
    }
}

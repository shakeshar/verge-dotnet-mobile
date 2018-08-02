using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Input;
using Verge.Core.Client;
using Verge.Mobile.Services;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{

    public class RPCLoginViewModel : BaseViewModel
    {
      
        private bool canStart = true;
        private IRPCCredentials model;

        #region Properties
        public ICommand LoginCmd { get; private set; }
        public ICommand GuestCmd { get; private set; }
        public string Username
        {
            get { return model?.Username ; }
            set { model.Username = value; base.OnPropertyChanged(); }
        }
        public string Url
        {
            get { return model?.Url; }
            set { model.Url = value; base.OnPropertyChanged(); }
        }
        public string Password
        {
            get { return model?.Password; }
            set { model.Password = value; base.OnPropertyChanged(); }
        }
        public int Port
        {
            get { return model.Port; }
            set { model.Port = value; base.OnPropertyChanged(); }
        }
        #endregion
        public RPCLoginViewModel()
        {
            LoginCmd = new Command(async () => await Login(), () => canStart);
            model = Storage.GetItem<RPCCredentials>(ConstantStrings.RPC_LOGIN_CREDENTIALS_KEY);
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
                //Application.Current.Properties[LOGIN_CREDENTIALS_PASSWORD] = Password;
                //Application.Current.Properties[LOGIN_CREDENTIALS_USERNAME] = Username;
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
    public class RPCCredentials :  IRPCCredentials
    {
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }

        public RPCCredentials()
        {
            
        }
    }
}

using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Verge.Core.Client;
using Verge.Mobile.Models;
using Verge.Mobile.Services;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{

    public class RPCLoginViewModel : BaseViewModel
    {
      
       
        private IRPCCredentials model;
        IVergeClient status;
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
            LoginCmd = new Command(async () => await Login(), () => CanStart);
            model = Storage.GetItem<RPCCredentials>(ConstantStrings.RPC_LOGIN_CREDENTIALS_KEY);

            //fix
            status = ViewModelLocator.Resolve<IVergeClient>();
            
        }
        private async Task Login()
        {
            status = ViewModelLocator.Resolve<IVergeClient>();
            CanStart = false;
            IsBusy = true;
            ((Command)LoginCmd).ChangeCanExecute();
            try
            {
                var response = await status.GetInfo();
                if (response.Response.IsSuccessStatusCode)
                {
                    //CLEAN UP!
                    //var accounts = await status.ListAccounts();
                    //var account = accounts.Data.Result.Keys.ToList();
                    //var result = await base.NavigationService.CurrentPage().DisplayActionSheet(ConstantStrings.PICK_ACCOUNT_TEXT_KEY, null, null, account.ToArray());
                    
                    //if (result == null) return;
                    //var transaction = ViewModelLocator.Resolve<ITransaction>();
                    //transaction.SetAccount(result);
                }
                await NavigationService.NavigateToAsync<MainViewModel>();
            }
            catch (Exception e)
            {
              await NavigationService.Display("Can't connect, check credentials");

            }
            //App.Account = new Domain.Strat(Username, NBitcoin.Network.TestNet);
            try
            {
                //Application.Current.Properties[LOGIN_CREDENTIALS_PASSWORD] = Password;
                //Application.Current.Properties[LOGIN_CREDENTIALS_USERNAME] = Username;
            }
            catch (Exception e)
            {
              await  NavigationService.Display(e.Message);
            }
            CanStart = true;
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

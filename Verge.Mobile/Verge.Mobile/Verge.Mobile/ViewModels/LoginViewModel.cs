﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{
    public class LoginViewModel : CollectionViewModel<object>
    {
        private bool canStart = true;
       
      
        public ICommand LoginCmd { get; private set; }
        public ICommand RPCLoginPageCmd { get; private set; }
        public ICommand GuestCmd { get; private set; }
        public string Username
        {
            get { return model?.Username; }
            set { model.Username = value; base.OnPropertyChanged(); }
        }

        public string Password
        {
            get { return model?.Password; }
            set { model.Password = value; base.OnPropertyChanged(); }
        }

        IRPCCredentials model;
        public LoginViewModel()
        {
            LoginCmd = new Command(async () => await Login(), () => canStart);
            RPCLoginPageCmd = new Command(async () => await NavigationService.NavigateToAsync<RPCLoginViewModel>(false));

            model = Storage.GetItem<RPCCredentials>("test");
           
        }
        public override async Task OnApperaing()
        {
           
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
}

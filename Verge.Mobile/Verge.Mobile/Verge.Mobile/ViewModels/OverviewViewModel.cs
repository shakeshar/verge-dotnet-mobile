using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Input;
using Verge.Mobile.Models;
using Verge.Mobile.Services;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{

    public class OverviewViewModel : BaseViewModel
    {
       
        private IOverviewStatus model;

        #region Properties
        public ICommand LoginCmd { get; private set; }
        
        public string Balance
        {
            get { return $"{model?.Balance} XVG"; }
        }
        public string Unconfirmed
        {
            get { return $"{model?.Unconfirmed} XVG"; }
        }
        #endregion
        public OverviewViewModel()
        {
            LoginCmd = new Command(async () => await Login(), () => CanStart);
            model = Storage.GetItem<OverviewStatus>("overview");
        }
        private async Task Login()
        {
            CanStart = false;
            IsBusy = true;
            ((Command)LoginCmd).ChangeCanExecute();
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
            CanStart = true;
            IsBusy = false;
            ((Command)LoginCmd).ChangeCanExecute();
        }
    }
   
}

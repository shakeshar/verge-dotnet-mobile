using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Input;
using Verge.Core.Client;
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
        public ICommand AddNodeCmd { get; private set; }


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
            AddNodeCmd = new Command(async () => await NavigationService.NavigateToAsync<EditNodeViewModel>());
            LoginCmd = new Command(async () => await Login(), () => CanStart);
            model = ViewModelLocator.Resolve<IOverviewStatus>();
            model.OnReload += Model_OnReload;
        }

        private void Model_OnReload(object sender, EventArgs e)
        {
            base.OnPropertyChanged(nameof(Balance));
            base.OnPropertyChanged(nameof(Unconfirmed));

        }

        public override async Task OnApperaing()
        {
            IsBusy = true;
            base.OnApperaing();
            await model.Load();
            base.OnPropertyChanged(nameof(Balance));
            IsBusy = false;
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

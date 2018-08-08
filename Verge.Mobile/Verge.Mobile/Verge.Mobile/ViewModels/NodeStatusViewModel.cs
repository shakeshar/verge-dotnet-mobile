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

    public class NodeStatusViewModel : CollectionViewModel<NodeStatusItemViewModel>
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
        public NodeStatusViewModel()
        {
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
    public class NodeStatusItemViewModel : BaseViewModel
    {
        #region Fields
        private string name;
        private string url;
        private string ip;
        private string lastSeen;
        private string blocks;
        #endregion

        #region Properties
        public string Name { get { return name; } set { name = value; OnPropertyChanged(); } } 
        public string Url { get { return url; } set { url = value; OnPropertyChanged(); } }
        public string IP { get { return ip; } set { ip = value; OnPropertyChanged(); } }
        public string LastSeen { get { return lastSeen; } set { lastSeen = value; OnPropertyChanged(); } }
        public string Blocks { get { return blocks; } set { blocks = value; OnPropertyChanged(); } }
        #endregion
        public NodeStatusItemViewModel()
        {

        }

    }

}

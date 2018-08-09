using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{
    public class EditNodeViewModel :  BaseViewModel
    {
        private RPCCredentials item;
        public RPCCredentials Item { get { return item; } set { item = value; } } 
        public ICommand SaveCmd { get; private set; }
        public ICommand RemoveCmd { get; private set; }

        List<RPCCredentials> hmm;
        bool isEditMode = false;
        public EditNodeViewModel()
        {
            Item = new RPCCredentials();
           hmm = Storage.GetItem<List<RPCCredentials>>(ConstantStrings.NODE_STATUS_KEY);
            SaveCmd = new Command(async () => await Save(), () => CanStart);
            RemoveCmd = new Command(async () => await Remove(), () => CanStart);
            if (((App)App.Current).Cred != null)
            {
                isEditMode = true;
                Item = ((App)App.Current).Cred;
                ((App)App.Current).Cred = null;
            }
            
        }
        private async Task Remove()
        {

            CanStart = false;
            IsBusy = true;
            ((Command)SaveCmd).ChangeCanExecute();

            try
            {
                hmm.Remove(Item);
                await NavigationService.NavigateToAsync<NodeStatusViewModel>();
            }
            catch (Exception e)
            {
                await NavigationService.Display(e.Message);
            }
            CanStart = true;
            IsBusy = false;
            ((Command)SaveCmd).ChangeCanExecute();
        }
        private async Task Save()
        {
            
            CanStart = false;
            IsBusy = true;
            ((Command)SaveCmd).ChangeCanExecute();
            
            try
            {
                if (!isEditMode)
                    hmm.Add(Item);
                
                await NavigationService.NavigateToAsync<NodeStatusViewModel>();
            }
            catch (Exception e)
            {
                await NavigationService.Display(e.Message);
            }
            CanStart = true;
            IsBusy = false;
            ((Command)SaveCmd).ChangeCanExecute();
        }
    }
}

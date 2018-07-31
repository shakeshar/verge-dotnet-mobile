using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Verge.Mobile.Models;
using Verge.Mobile.Services;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{
    public class SendViewModel : CollectionViewModel<object>
    {
      
        public IDataStore<Contact> DataStore;
        private bool canStart = true;
        private string username;
        private string max;
        private string message;
        private decimal amount;
        private decimal fee;
        private Contact contact;
        public ICommand NewContactCmd { get; private set; }
        public ICommand SendCmd { get; private set; }
        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Message { get => message; set => SetProperty(ref message, value); }
        public string Max { get => max; set => SetProperty(ref max, value); }
        public decimal Amount { get => amount; set => SetProperty(ref amount, value); }
        public decimal Fee { get => fee; set => SetProperty(ref fee, value); }
        public Contact Contact { get => contact; set => SetProperty(ref contact, value); } 
        public SendViewModel(IDataStore<Contact> dataStore)
        {
            this.Fee = 0.001m;
            this.DataStore = dataStore;
            SendCmd = new Command(async () => await Send(), () => canStart);
            NewContactCmd = new Command(async () => await NewContact(), () => canStart);
       
            Init();
        }
        public override async Task OnApperaing()
        {
            await Init();
            Contact = (Contact)Items.Last();
        }
       
        private async Task Init()
        {
            Items.Clear();
            var result =await DataStore.GetItemsAsync();
            foreach (var item in result)
            {
                Items.Add(item);
            }
            if (Items.Count < 1)
                Items.Add(new Contact() { Address = "myHJwGPMdxLDPqj93fMyANvvmhGoQA8YsL", Name = "Testnet account" });
            //Max = await App.Account.GetBalanceSummaryString();
        }
        private async Task Send()
        {
            canStart = false;
            IsBusy = true;
            ((Command)SendCmd).ChangeCanExecute();
            //var result = await App.Account.Send(Contact.Address, new NBitcoin.Money(Amount, NBitcoin.MoneyUnit.BTC), new Money(Fee, MoneyUnit.BTC), Message);
            //if (result.Success)
            //{
            //    await NavigationService.Display("Success!");
            //}
            canStart = true;
            IsBusy = false;
            ((Command)SendCmd).ChangeCanExecute();
        }
        private async Task NewContact()
        {
            try {
                canStart = false;
                IsBusy = true;
                await NavigationService.NavigateToAsync<NewContactViewModel>();
                ((Command)SendCmd).ChangeCanExecute();

                canStart = true;
                IsBusy = false;
                ((Command)SendCmd).ChangeCanExecute();
            }
            catch (Exception e)
            {

            }
            }
    }
}

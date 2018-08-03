using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Verge.Core.Client;
using Verge.Mobile.Models;
using Verge.Mobile.Services;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{
    public class SendViewModel : CollectionViewModel<object>
    {
      
        
     
   
        private string max;
        private string message;
        private string address;
        private decimal amount;
        private decimal fee;
        private Contact contact;

        public Contact Contact { get => contact; set => SetProperty(ref contact, value); }
        public ICommand SendCmd { get; private set; }
       
        public string Message { get => message; set => SetProperty(ref message, value); }
        public string Max { get => max; set => SetProperty(ref max, value); }
        public string Address { get => address; set => SetProperty(ref address, value); }
        public decimal Amount { get => amount; set => SetProperty(ref amount, value); }
        public decimal Fee { get => fee; set => SetProperty(ref fee, value); }

        private readonly IVergeClient client;
        public SendViewModel()
        {
            this.Fee = 0.1m;
            client = ViewModelLocator.Resolve<IVergeClient>();
            SendCmd = new Command(async () => await Send(), () => CanStart);
           
        }
        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData is TransactionsItemViewModel)
            {
                TransactionsItemViewModel item = (TransactionsItemViewModel)navigationData;
                //Items.Clear();
                //Items.Add(new Contact()
                //{
                //    Address = item.Address,
                //    Name = item.Address
                //});
                Address = item.Address;
                //Contact = new Contact() { Address = item.Address, Name = item.Address };
                OnPropertyChanged(nameof(Contact));
            }

        }
        
        private async Task Send()
        {
            CanStart = false;
            IsBusy = true;

            ((Command)SendCmd).ChangeCanExecute();
            //FIX
            await client.WalletPassphrase("supersecret");
            var response = await client.SendToAddress(Address, Amount, message, message);

            
            if (response.Response.IsSuccessStatusCode)
            {
                await NavigationService.Display("Success!");
                   
            }
            CanStart = true;
            IsBusy = false;
            ((Command)SendCmd).ChangeCanExecute();
        }
        //private async Task NewContact()
        //{
        //    try {
        //        CanStart = false;
        //        IsBusy = true;
        //        await NavigationService.NavigateToAsync<NewContactViewModel>();
        //        ((Command)SendCmd).ChangeCanExecute();

        //        CanStart = true;
        //        IsBusy = false;
        //        ((Command)SendCmd).ChangeCanExecute();
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    }
    }
}

using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Verge.Mobile.Models;
using Verge.Mobile.Services;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{
    public class TransactionsViewModel : CollectionViewModel<TransactionsItemViewModel>
    {
        
        public ICommand ReloadCmd { get; private set; }
        ITransaction model;
        public TransactionsViewModel()
        {
            model = ViewModelLocator.Resolve<ITransaction>();
            ReloadCmd = new Command(async () => await Load() );
            Load();
            
        }
        public override async Task OnApperaing()
        {
            await Load();
        }
        public async Task Load()
        {
            IsBusy = true;
            
            await model.Load("main");
            foreach (var item in model.Transactions)
            {
                Items.Add(new TransactionsItemViewModel()
                {
                    Address = item.address,
                    Amount = $"{Convert.ToDecimal(item.amount)} XVG",
                    Date = DateTimeOffset.FromUnixTimeMilliseconds((long)item.timereceived).ToString()
                });
            }
            IsBusy = false;
        }
    }
    public enum TransactionType
    {
        From,
        To
    }
    public class TransactionsItemViewModel
    {
        public TransactionType TransactionType { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public string Amount { get; set; }

    }

}

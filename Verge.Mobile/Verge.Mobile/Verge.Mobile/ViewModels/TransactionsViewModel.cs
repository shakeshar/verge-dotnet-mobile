using System;
using System.Linq;
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
        public ICommand SelectedItemCmd { get; private set; }
        ITransaction model;
        public TransactionsViewModel()
        {
            model = ViewModelLocator.Resolve<ITransaction>();
            ReloadCmd = new Command(async () => await Load() );
            SelectedItemCmd = new Command(async () =>
            {
                await NavigationService.NavigateToAsync<SendViewModel>(SelectedItem);
            });
            
        }

        public async Task Load()
        {
            IsBusy = true;
            var balance = ViewModelLocator.Resolve<IOverviewStatus>();
            await balance.Load();
            await model.Load();
            Items.Clear();
            foreach (var item in model.Transactions.OrderByDescending(px => px.timereceived))
            {
                if (item.category == null) continue;
                double total = item.amount + item.fee;
                Items.Add(new TransactionsItemViewModel()
                {
                    TransactionType = (item.category.Length == 4 ? TransactionType.Send : TransactionType.Receive),
                    Address = item.address,
                    Amount = $"{Convert.ToDecimal(total)} XVG",
                    Category = item.category,
                    Date = DateTimeOffset.FromUnixTimeSeconds(item.timereceived).ToString()
                });
            }
            IsBusy = false;
        }
    }
    public enum TransactionType
    {
        Send,
        Receive
    }
    public class TransactionsItemViewModel
    {
        public TransactionType TransactionType { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public string Amount { get; set; }
        public string Category { get; set; }

    }

}

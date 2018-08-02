using System;
using System.Threading.Tasks;
using Verge.Mobile.Models;
using Verge.Mobile.Services;

namespace Verge.Mobile.ViewModels
{
    public class TransactionsViewModel : CollectionViewModel<TransactionsItemViewModel>
    {


        public TransactionsViewModel()
        {
            Transaction();
        }
        public override async Task OnApperaing()
        {
            await Transaction();
        }
        private async Task Transaction()
        {
            IsBusy = true;
            Items.Add(new TransactionsItemViewModel()
            {
                Amount = 10m,
                Address = "DQmAPDFTEkYVAdfT3WdRoTGb9XpcKoRGJ2",
                 Date = DateTime.Now,
                  TransactionType = TransactionType.From
                   
            });
            Items.Add(new TransactionsItemViewModel()
            {
                Amount = 10m,
                Address = "DQmAPDFTEkYVAdfT3WdRoTGb9XpcKoRGJ2",
                Date = DateTime.Now,
                TransactionType = TransactionType.To
            });
            Items.Add(new TransactionsItemViewModel()
            {
                Amount = 10m,
                Address = "DQmAPDFTEkYVAdfT3WdRoTGb9XpcKoRGJ2",
                Date = DateTime.Now,
                TransactionType = TransactionType.To
            });
            Items.Add(new TransactionsItemViewModel()
            {
                Amount = 10m,
                Address = "DQmAPDFTEkYVAdfT3WdRoTGb9XpcKoRGJ2",
                Date = DateTime.Now,
                TransactionType = TransactionType.From
            });
            //var result = await App.Account.GetTransactions();
            //foreach (var item in result.Operations)
            //{
            //    Items.Add(new
            //    {
            //        Amount = item.Amount.ToString(),
            //        Confirmations = item.Confirmations.ToString(),
            //        FirstSeen = item.FirstSeen.ToString(),
            //        TransactionId = item.TransactionId.ToString()
            //    });              
            //}
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
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }

    }

}

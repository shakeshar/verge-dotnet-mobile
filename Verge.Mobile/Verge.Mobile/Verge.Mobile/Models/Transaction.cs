using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verge.Core.Client;

namespace Verge.Mobile.Models
{
    public class Transaction : ITransaction
    {
        public string Account { get; private set; }
        public IList<Core.Contract.AccountTransactionsResponse> Transactions { get; private set; } = new List<Core.Contract.AccountTransactionsResponse>();

        IVergeClient client;
        public Transaction()
        {
            client = ViewModelLocator.Resolve<IVergeClient>();
        }

        public async Task Load()
        {
            var response = await client.ListTransactions("*", 30);
            Transactions = response.Data.Result.ToList();
        }

        public void SetAccount(string account)
        {
            this.Account = account;
        }
    }

    public interface ITransaction
    {
        IList<Core.Contract.AccountTransactionsResponse> Transactions { get; }
        string Account { get; }
        Task Load();
        void SetAccount(string account);
    
    }
}

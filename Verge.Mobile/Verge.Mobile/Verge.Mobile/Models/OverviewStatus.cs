using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Verge.Core.Client;

namespace Verge.Mobile.Models
{
    public class OverviewStatus : IOverviewStatus
    {
        public decimal Balance { get; private set; }
        public decimal Unconfirmed { get; private set; }
        IVergeClient client;
        public OverviewStatus()
        {
            client = ViewModelLocator.Resolve<IVergeClient>();
        }

        public async Task Load()
        {
            var response = await client.GetBalance();
            Balance = response.Data.Result;
        }
    }

    public interface IOverviewStatus
    {
        decimal Balance { get;  }
        decimal Unconfirmed { get;  }
        Task Load();
    }
}

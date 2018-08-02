using System;
using System.Collections.Generic;
using System.Text;

namespace Verge.Mobile.Models
{
    public class OverviewStatus : IOverviewStatus
    {
        public decimal Balance { get; } = 234.20m;
        public decimal Unconfirmed { get; } = 10.0m;

        public OverviewStatus()
        {
           
        }
    }

    public interface IOverviewStatus
    {
        decimal Balance { get;  }
        decimal Unconfirmed { get;  }
    }
}

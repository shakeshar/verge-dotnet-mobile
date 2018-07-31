using System;
using System.Collections.Generic;
using System.Text;

namespace Verge.Mobile.Contracts
{
    public interface INotificationRegistration
    {
        string Device { get; }
        string TokenId { get; }
        IEnumerable<string> Tags { get; set; }
    }
}

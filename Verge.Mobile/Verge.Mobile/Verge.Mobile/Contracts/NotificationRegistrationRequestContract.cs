using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Verge.Mobile.Contracts
{
    [DataContract]
    public class NotificationRegistrationRequestContract : INotificationRegistration
    {
        [DataMember(Name = "tokenId")]
        public string TokenId { get; set; }
        [DataMember(Name = "device")]
        public string Device { get; set; }
        [DataMember(Name = "tags")]
        public IEnumerable<string> Tags { get; set; }
    }
}

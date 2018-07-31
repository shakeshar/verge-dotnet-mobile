using System;
using System.Collections.Generic;
using System.Text;

namespace Verge.Mobile.Services
{
    public class AccountServiceSettings 
    {
        public string BaseUri { get; private set; }
        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public bool IsMock { get; private set; }

        public AccountServiceSettings(string baseUri, bool isMock=false)
        {
            BaseUri = baseUri;
            IsMock = isMock;
        }
    }
}

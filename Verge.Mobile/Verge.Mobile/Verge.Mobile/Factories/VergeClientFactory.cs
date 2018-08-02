using System;
using System.Collections.Generic;
using System.Text;
using Verge.Core.Client;
using Verge.Mobile.Services;
using Verge.Mobile.ViewModels;

namespace Verge.Mobile.Factories
{
    public static class VergeClientFactory
    {
        public static IVergeClient Create()
        {
            var storage = ViewModelLocator.Resolve<IStorageService>();
            var cred = storage.GetItem<RPCCredentials>(ConstantStrings.RPC_LOGIN_CREDENTIALS_KEY);
            return new VergeClient(cred.Username, cred.Password, cred.Url, cred.Port);
        }
    }
}

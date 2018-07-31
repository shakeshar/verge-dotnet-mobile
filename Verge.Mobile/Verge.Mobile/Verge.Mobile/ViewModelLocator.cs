using Microsoft.Extensions.DependencyInjection;
using System;
using Verge.Mobile.Models;
using Verge.Mobile.Services;
using Verge.Mobile.ViewModels;

namespace Verge.Mobile
{
    public static class ViewModelLocator
    {
        private static ServiceProvider serviceProvider;
        private static ServiceCollection serviceProviderCollection = new ServiceCollection();
        static ViewModelLocator()
        {
            #region ViewModels
            serviceProviderCollection.AddSingleton<LoginViewModel>();
            serviceProviderCollection.AddSingleton<MainViewModel>();                    
            serviceProviderCollection.AddSingleton<NewContactViewModel>();

            serviceProviderCollection.AddTransient<TransactionsViewModel>();
            serviceProviderCollection.AddTransient<SendViewModel>();
            #endregion
            Func<IServiceProvider, IDataStore<Contact>> contactDataStore = (px) =>
            {
                //Ugly solution, fix this!
                string key = "contacts";
                if (!App.Current.Properties.ContainsKey(key))
                {
                    var store = new DataStore<Contact>();
                    App.Current.Properties.Add(key, store);
                    return store;
                }
                var x = App.Current.Properties[key];
                if (x != null)
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DataStore<Contact>>(x.ToString());
                    App.Current.Properties[key] = result;
                }
                else
                {
                    App.Current.Properties[key] = new DataStore<Contact>();
                }
                return App.Current.Properties[key] as DataStore<Contact>;

            };
          

            serviceProviderCollection.AddSingleton<INavigationService, NavigationService>();
            serviceProviderCollection.AddSingleton<IDataStore<Contact>>(contactDataStore);
            serviceProvider = serviceProviderCollection.BuildServiceProvider();
            SetTranslation();
        }
        public static void SetTranslation()
        {
            //Translation.CultureService.Content.Add("loginUsernamePlaceholder", "Ange användarnamn");
            //Translation.CultureService.Content.Add("loginPasswordPlaceholder", "Lösenord");
            //Translation.CultureService.Content.Add("loginButton", "LOGGA IN");
        }
        public static void SetMockup()
        {
            #region ViewModels
            serviceProviderCollection.AddSingleton<LoginViewModel>();
            serviceProviderCollection.AddSingleton<MainViewModel>();
            #endregion
        }
        public static T Resolve<T>()
        {
            return serviceProvider.GetService<T>();
        }
        public static object Resolve(Type t)
        {
            return serviceProvider.GetService(t);
        }


    }
}

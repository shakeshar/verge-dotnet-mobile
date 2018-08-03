﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Verge.Core.Client;
using Verge.Mobile.Factories;
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
            Func<IServiceProvider, IDictionary<string, object>> cacheFactory = (px) => App.Current.Properties;
            Func<IServiceProvider, IVergeClient> vergeClientFunc = (px) => VergeClientFactory.Create();
            
            #region ViewModels
            serviceProviderCollection.AddSingleton<PaymentViewModel>();
            serviceProviderCollection.AddSingleton<OverviewViewModel>();
            serviceProviderCollection.AddSingleton<TransactionsViewModel>();
            serviceProviderCollection.AddSingleton<LoginViewModel>();
            serviceProviderCollection.AddSingleton<MainViewModel>();                    
            serviceProviderCollection.AddSingleton<NewContactViewModel>();
            serviceProviderCollection.AddSingleton<RPCLoginViewModel>();
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
            
            serviceProviderCollection.AddSingleton(typeof(IStorageService), typeof(StorageService));
            serviceProviderCollection.AddSingleton<IDictionary<string, object>>(cacheFactory);
            serviceProviderCollection.AddSingleton<INavigationService, NavigationService>();
            serviceProviderCollection.AddSingleton<IDataStore<Contact>>(contactDataStore);         
            serviceProviderCollection.AddSingleton<IOverviewStatus, OverviewStatus>();
            serviceProviderCollection.AddSingleton<ITransaction, Transaction>();

            serviceProviderCollection.AddTransient<IVergeClient>(vergeClientFunc);
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
    public static class ViewModelDesign
    {
     
        private static TransactionViewModelMock monk;
        public static TransactionViewModelMock TransactionViewModel => monk ?? (monk = new TransactionViewModelMock());
        public static OverviewStatus OverviewStatus = new OverviewStatus();
  
    }
    public class TransactionViewModelMock 
    {
        public ObservableCollection<TransactionsItemViewModel> Items { get; set; } = new ObservableCollection<TransactionsItemViewModel>();
        public TransactionViewModelMock()
        {
            Items.Add(new TransactionsItemViewModel()
            {
                Address = "adajölsdjöalsdjl",
                Amount = "100 XVG",
                Date = DateTime.Now.ToString(),
                TransactionType = TransactionType.To
            });
            Items.Add(new TransactionsItemViewModel()
            {
                Address = "adajölsdjöalsdjl",
                Amount = "100 XVG",
                Date = DateTime.Now.ToString(),
                TransactionType = TransactionType.From
            }); Items.Add(new TransactionsItemViewModel()
            {
                Address = "adajölsdjöalsdjl",
                Amount = "100 XVG",
                Date = DateTime.Now.ToString(),
                TransactionType = TransactionType.To
            }); Items.Add(new TransactionsItemViewModel()
            {
                Address = "adajölsdjöalsdjl",
                Amount = "100 XVG",
                Date = DateTime.Now.ToString(),
                TransactionType = TransactionType.From
            });
        }
    }
    public class TransactionViewModelItemMock
    {
        public string From { get; set; }
        public decimal Balance { get; set; }

    }
   
}

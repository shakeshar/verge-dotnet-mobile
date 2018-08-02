using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

            #region ViewModels
            serviceProviderCollection.AddSingleton<PaymentViewModel>();
            serviceProviderCollection.AddSingleton<OverviewViewModel>();
            serviceProviderCollection.AddSingleton<TransactionsViewModel>();
            serviceProviderCollection.AddSingleton<LoginViewModel>();
            serviceProviderCollection.AddSingleton<MainViewModel>();                    
            serviceProviderCollection.AddSingleton<NewContactViewModel>();
            serviceProviderCollection.AddSingleton<RPCLoginViewModel>();

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


            serviceProviderCollection.AddSingleton(typeof(IStorageService), typeof(StorageService));
            serviceProviderCollection.AddSingleton<IDictionary<string, object>>(cacheFactory);
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
        static ServiceReportListMockViewModel monkeysVM;
        static OverviewStatus overviewStatus;
        static TransactionViewModelMock transactionViewModel;

        public static ServiceReportListMockViewModel MonkeysViewModel => monkeysVM ?? (monkeysVM = new ServiceReportListMockViewModel());
        public static OverviewStatus OverviewStatus => overviewStatus ?? (overviewStatus = new OverviewStatus());
        public static TransactionViewModelMock TransactionViewModel => transactionViewModel ?? (transactionViewModel = new  TransactionViewModelMock());
    }
    public class TransactionViewModelMock : CollectionViewModel<TransactionViewModelItemMock>
    {

        public TransactionViewModelMock()
        {
            Items.Add(new TransactionViewModelItemMock()
            {
                Balance = 10m,
                From = "Temp"
            });
            Items.Add(new TransactionViewModelItemMock()
            {
                Balance = 10m,
                From = "Temp"
            });
            Items.Add(new TransactionViewModelItemMock()
            {
                Balance = 10m,
                From = "Temp"
            });
            Items.Add(new TransactionViewModelItemMock()
            {
                Balance = 10m,
                From = "Temp"
            });
        }
    }
    public class TransactionViewModelItemMock
    {
        public string From { get; set; }
        public decimal Balance { get; set; }

    }
    public class ServiceReportListMockViewModel : CollectionViewModel<object>
    {
        public ServiceReportListMockViewModel() : base()
        {
            string descrtiption = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation";
    
            Items.Add(new 
            {
                Description = descrtiption,
                Id = 0,
                IsRead = true,
                RegTime = DateTimeOffset.UtcNow,
                Status = "Mottaget"

            });

            Items.Add(new
            {
                Description = descrtiption,
                Id = 0,
                IsRead = true,
                RegTime = DateTimeOffset.UtcNow,
                Status = "Mottaget"

            });

            Items.Add(new
            {
                Description = descrtiption,
                Id = 0,
                IsRead = true,
                RegTime = DateTimeOffset.UtcNow,
                Status = "Mottaget"

            });

            Items.Add(new
            {
                Description = descrtiption,
                Id = 0,
                IsRead = true,
                RegTime = DateTimeOffset.UtcNow,
                Status = "Mottaget"

            });

        }
    }
}

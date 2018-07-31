using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Verge.Mobile.ViewModels;
using Verge.Mobile.Views;
using Xamarin.Forms;

namespace Verge.Mobile.Services
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>(bool push= true) where TViewModel : BaseViewModel;
        Task NavigateToAsync(Type viewmodel, bool push = true);
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task Display(string message);
        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
    public class NavigationService : INavigationService
    {
        //private readonly ISettingsService _settingsService;
        public async Task Display(string message)
        {
           await Application.Current.MainPage.DisplayAlert("", message, "ok");
        }
        public BaseViewModel PreviousPageViewModel
        {
            get
            {
                
                var mainPage = Application.Current.MainPage as MasterDetailPage;
                var viewModel = mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as BaseViewModel;
            }
        }
        public Task InitializeAsync()
        {
            return NavigateToAsync<LoginViewModel>();
        }

        public Task NavigateToAsync<TViewModel>(bool push = true) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null, push);
        }
        public Task NavigateToAsync(Type type, bool push = true)
        {
            return InternalNavigateToAsync(type, null, push);
        }
        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public async Task RemoveLastFromBackStackAsync()
        {
            if (Application.Current.MainPage is LoginPage) await  Task.FromResult(true);
            var mainPage = Application.Current.MainPage as MainPage;

            if (mainPage != null)
            {
               await mainPage.Detail.Navigation.PopAsync();
            }

            await Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            if (Application.Current.MainPage is LoginPage) return Task.FromResult(true);
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter, bool push = true)
        {
            Page page = CreatePage(viewModelType, parameter);


            if (page is LoginPage)
            {
                Application.Current.MainPage = page;
            }
            else if (page is MainPage)
            {
                Application.Current.MainPage = page;
            }            
            else
            {
                var navigationPage = Application.Current.MainPage as MasterDetailPage;
                if (navigationPage != null)
                {
                    if (push)
                        await navigationPage.Detail.Navigation.PushAsync(page);
                    else
                        navigationPage.Detail = new NavigationPage(page);
                }
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var name = viewModelType.FullName;
            var viewName = viewModelType.FullName.Replace(".ViewModels", ".Views").Replace("ViewModel", "Page");
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            page.BindingContext = ViewModelLocator.Resolve(viewModelType);

            return page;
        }
    }
}

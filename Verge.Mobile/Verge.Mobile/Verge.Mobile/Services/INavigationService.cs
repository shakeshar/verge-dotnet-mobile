using System;
using System.Globalization;
using System.Linq;
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
        Task NavigateToAsync<TViewModel>(bool push = true) where TViewModel : BaseViewModel;
        Task NavigateToAsync(Type viewmodel, bool push = true);
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task Display(string message);
        Task RemoveLastFromBackStackAsync();
        Task RemoveCurrent();
        Page CurrentPage();

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

                var mainPage = Application.Current.MainPage as NavigationPage;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as BaseViewModel;
            }
        }
        public Task InitializeAsync()
        {
            return NavigateToAsync<RPCLoginViewModel>();
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


        public Task RemoveLastFromBackStackAsync()
        {
            if (Application.Current.MainPage is LoginPage) return Task.FromResult(true);
            var mainPage = Application.Current.MainPage as MasterDetailPage;

            if (mainPage != null)
            {
                mainPage.Detail.Navigation.RemovePage(
                    mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }
        public async Task RemoveCurrent()
        {
            if (Application.Current.MainPage is LoginPage) await Task.FromResult(true);
            var mainPage = Application.Current.MainPage as MasterDetailPage;

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
            var currentPage = CurrentPage();
            if (currentPage?.BindingContext is BaseViewModel)      (currentPage.BindingContext as BaseViewModel).OnDisappearing();
                Page page = CreatePage(viewModelType, parameter);



            if (page is LoginPage)
            {
                Application.Current.MainPage = page;
            }

            else if (Application.Current.MainPage is MasterDetailPage)
            {
                var navigationPage = Application.Current.MainPage as MasterDetailPage;
                if (navigationPage != null)
                {
                    //Hmmmm dosen't work as intendet
                    if (push)
                        await navigationPage.Detail.Navigation.PushAsync(page);
                    else
                        navigationPage.Detail = new NavigationPage(page);
                }
            }
            else
            {
                Application.Current.MainPage = page;
            }
            BaseViewModel viewmodel = (page.BindingContext as BaseViewModel);
            await viewmodel.InitializeAsync(parameter);
            viewmodel.OnApperaing();
        }
        public Page CurrentPage()
        {
            var navigation = Application.Current.MainPage as MasterDetailPage;
            if (navigation == null) return Application.Current.MainPage;
            return ((NavigationPage)navigation.Detail).CurrentPage;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            viewName =viewName.Remove( viewName.Length - 4, 4) + "Page";
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
            var x = ViewModelLocator.Resolve(viewModelType);
            if (x == null) throw new Exception("ViewModel not configured");
            
            page.BindingContext = x;

            return page;
        }
    }
}

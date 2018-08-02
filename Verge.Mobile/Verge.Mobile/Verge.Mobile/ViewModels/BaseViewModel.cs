using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Verge.Mobile.Services;

namespace Verge.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        protected readonly IStorageService Storage;
        string title = string.Empty;
        bool isBusy = false;
        public bool CanStart = true;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        protected bool SetPropertyy<T>(out T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        public virtual async Task OnApperaing()
        {

        }
        public virtual async Task OnDisappearing()
        {

        }




        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
        public BaseViewModel()
        {

            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            Storage = ViewModelLocator.Resolve<IStorageService>();
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            test(() => { });
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public void test(Action action)
        {

        }
        #endregion
    }
}

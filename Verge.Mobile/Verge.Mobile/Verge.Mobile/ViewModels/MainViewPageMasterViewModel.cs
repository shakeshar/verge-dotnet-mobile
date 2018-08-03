using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Verge.Mobile.ViewModels
{
    public class MainViewPageMasterViewModel : BaseViewModel
    {
        public ObservableCollection<MainViewPageMenuItem> MenuItems { get; set; }

        public MainViewPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<MainViewPageMenuItem>(new[]
            {
                new MainViewPageMenuItem(typeof(OverviewViewModel)) {  Id = 0, Title = "Start".ToUpper() },
                    //new MainViewPageMenuItem(typeof(TransactionsViewModel)) {  Id = 0, Title = "Transactions".ToUpper() },
                    new MainViewPageMenuItem(typeof(SendViewModel)) {   Id = 0, Title = "Send".ToUpper() },
                    new MainViewPageMenuItem(typeof(LoginViewModel)) { Id = 0, Title = "Logout".ToUpper() }
            });
        }


    }
}

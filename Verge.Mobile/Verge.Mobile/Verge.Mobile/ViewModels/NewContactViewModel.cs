using System.Threading.Tasks;
using System.Windows.Input;
using Verge.Mobile.Models;
using Verge.Mobile.Services;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{
    public class NewContactViewModel : CollectionViewModel<object>
    {
        public IDataStore<Contact> DataStore;

        private bool canStart = true;
        private string name;
        private string address;
        public ICommand SaveCmd { get; private set; }
        public ICommand GenerateCmd { get; private set; }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Address { get => address; set => SetProperty(ref address, value); }

        private const string LOGIN_CREDENTIALS_USERNAME = "LOGIN_CREDENTIALS_USERNAME";
        private const string LOGIN_CREDENTIALS_PASSWORD = "LOGIN_CREDENTIALS_PASSWORD";
        public NewContactViewModel(IDataStore<Contact> dataStore)
        {
            DataStore = dataStore;
            SaveCmd = new Command(async () => await Save(), () => canStart);
            GenerateCmd = new Command(async () => await GenerateAddress(), () => canStart);
        }
        private async Task GenerateAddress()
        {
            //Address = Strat.CreateKey(NBitcoin.Network.TestNet).PubKey.ToString();
        }


        private async Task Save()
        {
            canStart = false;
            IsBusy = true;
            ((Command)SaveCmd).ChangeCanExecute();

            await DataStore.AddItemAsync(new Contact() { Address = address, Name = name });

            this.Address = string.Empty;
            this.name = string.Empty;
            var x = NavigationService.PreviousPageViewModel;
            await x.OnApperaing();
            await NavigationService.RemoveLastFromBackStackAsync();
            canStart = true;
            IsBusy = false;
            ((Command)SaveCmd).ChangeCanExecute();
        }
    }
}

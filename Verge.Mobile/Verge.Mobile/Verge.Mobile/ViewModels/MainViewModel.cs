using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{
    public class MainViewModel : CollectionViewModel<object>
    {
        private bool canStart = true;
        private string username;
        private string password;
        public ICommand LoginCmd { get; private set; }
        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }

        public MainViewModel()
        {
            LoginCmd = new Command(async () => await Login(), () => canStart);
        }
        private async Task Login()
        {
            canStart = false;
            ((Command)LoginCmd).ChangeCanExecute();
            canStart = true;
            ((Command)LoginCmd).ChangeCanExecute();
        }
    }
}

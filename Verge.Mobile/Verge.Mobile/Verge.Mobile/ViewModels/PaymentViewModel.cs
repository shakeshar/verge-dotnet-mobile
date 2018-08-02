using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Verge.Mobile.Models;
using Verge.Mobile.Services;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        public ICommand SendCmd { get; private set; }
        public ICommand ReceiveCmd { get; private set; }

        public PaymentViewModel()
        {
            SendCmd = new Command(() => NavigationService.Display("Not implemented"));
            ReceiveCmd = new Command(() => NavigationService.Display("Not implemented"));
        }
    }
}

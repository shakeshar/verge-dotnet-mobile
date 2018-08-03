using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verge.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Verge.Mobile.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaymentView : ContentView
	{
        PaymentViewModel viewmodel;
        public PaymentView ()
		{
            InitializeComponent();
#if (DESIGN)
            {
                return;
            }
#endif
            viewmodel = ViewModelLocator.Resolve<PaymentViewModel>();
            BindingContext = viewmodel;
        }
	}
}
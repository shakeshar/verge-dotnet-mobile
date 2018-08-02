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
	public partial class TransactionView : ContentView
	{
        private TransactionsViewModel viewmodel;
        public TransactionView()
        {
            InitializeComponent();
            viewmodel = ViewModelLocator.Resolve<TransactionsViewModel>();
            BindingContext = viewmodel;

        }
    }
}
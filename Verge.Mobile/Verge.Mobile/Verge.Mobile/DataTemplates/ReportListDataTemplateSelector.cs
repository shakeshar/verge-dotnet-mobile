using System;
using System.Collections.Generic;
using System.Text;
using Verge.Mobile.ViewModels;
using Xamarin.Forms;

namespace Verge.Mobile.DataTemplates
{
    public class ReportListDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }

        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
           switch(((TransactionsItemViewModel)item).TransactionType)
            {
                case TransactionType.Receive:
                    return ValidTemplate;
                case TransactionType.Send:
                    return InvalidTemplate;
                default:
                    return InvalidTemplate;
            }
        }
    }
}

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
                case TransactionType.From:
                    return ValidTemplate;
                case TransactionType.To:
                    return InvalidTemplate;
                default:
                    return InvalidTemplate;
            }
        }
    }
}

using System.Collections.ObjectModel;

namespace Verge.Mobile.ViewModels
{
    public class CollectionViewModel<T> : BaseViewModel
    {
        public ObservableCollection<T> Items { get; set; }
        public CollectionViewModel()
        {
            Items = new ObservableCollection<T>();
        }
    }
}

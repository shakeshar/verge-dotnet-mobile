using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Verge.Mobile.Services
{
    public class DataStore<T> : IDataStore<T>
    {
        public List<T> Items { get; set; }  
        public DataStore()
        {
            if (Items == null) Items = new List<T>();
        }
        public async Task<bool> AddItemAsync(T item)
        {
            Items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(T item)
        {
            throw new NotImplementedException();
            return await Task.FromResult(true);
        }

        public async Task<T> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            return Items;
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}

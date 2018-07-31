using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verge.Mobile.Clients
{
    public interface IMemoryCache<Key>
    {
        void Add<T>(Key key, T item, MemoryCacheOptions options = null) where T : class;
        void Remove(Key key);
        bool Exist(Key key);
        T Get<T>(Key key) where T : class;
        Task Load();
        Task Save();
        void Clear();
        void ClearAll();
    }
    public class MemoryCache<Key> : IMemoryCache<Key>
    {
        private Dictionary<Key, MemoryCacheItem> items;
        private readonly IStorageRepository<Dictionary<Key, MemoryCacheItem>> _repository;
        public MemoryCache(IStorageRepository<Dictionary<Key, MemoryCacheItem>> repository)
        {
            _repository = repository;
            items = new Dictionary<Key, MemoryCacheItem>();
        }

        public T Get<T>(Key key)
            where T : class
        {
            if (!Exist(key)) return default(T);
            MemoryCacheItem item = items[key];
            if (item.IsExpired) return default(T);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Newtonsoft.Json.JsonConvert.SerializeObject(item.Item));
            return result;
        }
        public void Add<T>(Key key, T item, MemoryCacheOptions options = null)
           where T : class
        {
            if (options == null) options = MemoryCacheOptions.Default();
            items[key] = new MemoryCacheItem(options, item);

        }
        public void Remove(Key key)
        {
            if (items.ContainsKey(key))
                items.Remove(key);
        }
        public bool Exist(Key key)
        {
            return (items.ContainsKey(key));
        }
        public async Task Load()
        {
            items = await _repository.Load();
            if (items == null) items = new Dictionary<Key, MemoryCacheItem>();
            if (items.Any(px => px.Value.Options == null)) items = new Dictionary<Key, MemoryCacheItem>();
        }
        public async Task Save()
        {
            if (!await _repository.Exist()) await _repository.Create();

            await _repository.Save(items);
        }

        public void Clear()
        {
            if (items == null) return;

            var cacheItems = items.Where(px => px.Value.Options.Importance == CacheImportance.Normal).Select(px => new { key = px.Key }).ToList();

            try
            {
                foreach (var item in cacheItems)
                {
                    items.Remove(item.key);
                }
            }
            catch (Exception e)
            {

            }
        }

        public void ClearAll()
        {
            items = new Dictionary<Key, MemoryCacheItem>();
        }
    }
    public class MemoryCacheOptions
    {
        public DateTime Expires { get; set; }
        public CacheImportance Importance { get; set; }



        public static MemoryCacheOptions Default()
        {
            return new MemoryCacheOptions() { Expires = DateTime.Now.AddSeconds(10), Importance = CacheImportance.Normal };
        }
    }
    [Flags]
    public enum CacheImportance
    {
        Normal = 1,
        Forever = 2
    }
    public class MemoryCacheItem
    {
        public MemoryCacheOptions Options { get; set; }
        public object Item { get; set; }
        public bool IsExpired => (0 < (DateTime.Now - Options.Expires).Ticks);
        public MemoryCacheItem()
        {

        }
        public MemoryCacheItem(MemoryCacheOptions options, object item)
        {
            if (options == null) options = MemoryCacheOptions.Default();
            Options = options;
            Item = item;
        }
    }
}

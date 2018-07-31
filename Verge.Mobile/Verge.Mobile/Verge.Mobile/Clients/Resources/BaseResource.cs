using System.Threading;
using System.Threading.Tasks;
using Verge.Mobile.Client.Clients;
using Verge.Mobile.Clients;
using Verge.Mobile.Contracts;

namespace Verge.Mobile.Client.Resources
{
    public abstract class BaseResource<T>
       where T : class
    {
        protected IJsonClient Client;
        public BaseResource()
        {

        }
        private readonly IMemoryCache<string> _cache;

        public BaseResource(IJsonClient client, IMemoryCache<string> cache = null)
        {
            _cache = cache;
            Client = client;
        }
        protected async Task<IClientBaseResponse<bool>> DeleteAsync(string url)
        {
            var requestUri = Client.BuildUri(url);

            return await Client.DeleteAsync(requestUri);
        }
   
        protected async Task<IClientBaseResponse<T>> GetAsync<T>(string url, string query = "", MemoryCacheOptions options = null)
        {
            if (options == null) options = new MemoryCacheOptions();
            var requestUri = Client.BuildUri(url, query);
            
            var result = await Client.GetAsync<T>(requestUri);
          
            return result;
        }
        protected async Task<IClientBaseResponse<T>> GetAsync(string url, CancellationToken cancellationToken, MemoryCacheOptions options = null)
        {
            if (options == null) options = new MemoryCacheOptions();
            var requestUri = Client.BuildUri(url);
            if (_cache != null)
            {
                var item = _cache.Get<T>(requestUri.AbsolutePath);
                if (item != null) return new ClientCacheResponse<T>(item);
            }
            var result = await Client.GetAsync<T>(requestUri, cancellationToken);
            if (_cache != null) _cache.Add<T>(requestUri.AbsolutePath, result.Data, options);
            return result;
        }
        protected async Task<IClientBaseResponse<T2>> PostAsync<T2>(string url, object entity)
        {
            if (_cache != null)
            {
                _cache.Remove(url);
            }

            var requestUri = Client.BuildUri(url);
            return await Client.PostAsync<T2>(requestUri, entity);
        }
        protected async Task<IClientBaseResponse<T>> PostAsync(string url, object entity) 
        {
            var requestUri = Client.BuildUri(url);
            return await Client.PostAsync<T>(requestUri, entity);
        }
        protected async Task<IClientBaseResponse<T>> PutAsync(string url, object entity)
        {
            var requestUri = Client.BuildUri(url);
            return await Client.PutAsync<T>(requestUri, entity);
        }


    }
}

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Verge.Mobile.Contracts;

namespace Verge.Mobile.Client.Clients
{
    public interface IJsonClient
    {
        Uri BuildUri(string handler, string query = "");
        HttpClient Client { get; }
        Task<IClientBaseResponse<bool>> DeleteAsync(Uri url);
        Task<IClientBaseResponse<T>> GetAsync<T>(Uri url);
        Task<IClientBaseResponse<T>> GetAsync<T>(Uri url, CancellationToken cancellationToken);
        Task<IClientBaseResponse<T>> PostAsync<T>(Uri url, object entity);      
        Task<IClientBaseResponse<T>> PutAsync<T>(Uri url, object entity);
    }
    
    public interface IInvokeHttpResponse : IJsonClient
    {
        Task<T> Invoke<T>(Task<HttpResponseMessage> task);
    }
}
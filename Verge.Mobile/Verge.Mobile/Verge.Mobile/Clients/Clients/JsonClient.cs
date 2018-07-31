using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Verge.Mobile.Contracts;

namespace Verge.Mobile.Client.Clients
{
    public class JsonClient : IJsonClient
    {
        public Uri BuildUri(string handler, string query = "")
        {

            return new UriBuilder(_baseUri)
            {
                Path = handler,
                Query = query
            }.Uri;
        }
        private readonly string _baseUri;
        private readonly Func<object, StringContent> httpContent;
        private HttpClient client;
        public HttpClient Client
        {
            get { return this.client; }
        }
        public JsonClient(string baseUri, Func<object, StringContent> httpContent, bool allowUnsecureSSL = false) :this(baseUri, httpContent)
        {        
           if (allowUnsecureSSL)
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        }
        public JsonClient(string baseUri, Func<object, StringContent> httpContent)
        {            
            client = new HttpClient();
            _baseUri = baseUri;
            this.httpContent = httpContent;
        }
        
        public JsonClient(string baseUri, HttpMessageHandler messageHandler)
        {
            client = new HttpClient(messageHandler);
            _baseUri = baseUri;
        }
        private StringContent CreateHttpContent(object entity)
        {
            return httpContent.Invoke(entity);
        }
        public async Task<IClientBaseResponse<T>>  GetAsync<T>(Uri url)
        {
                var result = await Invoke<T>(client.GetAsync(url));
                return result;
        }
        public async Task<IClientBaseResponse<T>> GetAsync<T>(Uri url, CancellationToken cancellationToken)
        {
            var result = await Invoke<T>(client.GetAsync(url, cancellationToken));
            return result;
        }
        public async Task<IClientBaseResponse<T>> PutAsync<T>(Uri url, object entity)
        {
            StringContent httpContent = CreateHttpContent(entity);
            var result = await Invoke<T>(client.PutAsync(url, httpContent));
            return result;
        }

        public async Task<IClientBaseResponse<T>> PostAsync<T>(Uri url, object entity)
        {
            StringContent httpContent = CreateHttpContent(entity);
            var result = await Invoke<T>(client.PostAsync(url, httpContent));
            return result;
        }
        public async Task<IClientBaseResponse<T2>> PostAsync<T, T2>(Uri url, T entity)
        {
            StringContent httpContent = CreateHttpContent(entity);
            var result = await Invoke<T2>(client.PostAsync(url, httpContent));
            return result;
        }
        public async Task<IClientBaseResponse<bool>> DeleteAsync(Uri url)
        {
           var response = await client.DeleteAsync(url);
            return new ClientSuccessResponse<bool>(response);
           
        }
      
        public virtual async Task<IClientBaseResponse<T>> Invoke<T>(Task<HttpResponseMessage> task)
        {
            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await task;
                IClientBaseResponse<T> response;
                if (responseMessage.IsSuccessStatusCode)
                    response = new ClientSuccessResponse<T>(responseMessage);
                else
                    response = new ClientFailedResponse<T>(responseMessage);
                await response.Read();
                return response;
            }
            catch (Exception e) { throw e; }
        }
    }
    public class JsonAuthClient : JsonClient
    {
        public JsonAuthClient(string baseUri, Dictionary<string, string> headers, Func<object, StringContent> httpContent) :base(baseUri, httpContent)
        {            
            foreach (var item in headers)
            {
                base.Client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
            }
        }
        public JsonAuthClient(string baseUri, Dictionary<string, string> headers, HttpMessageHandler handler):base(baseUri, handler)
        {
            foreach (var item in headers)
            {
                base.Client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
            }
        }
    }
}

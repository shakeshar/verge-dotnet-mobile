using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Verge.Mobile.Contracts;

namespace Verge.Mobile.Client
{
    public class ResponseMessage<T>:  IResponseMessage<T>
    {        
        public T Data { get; internal set; }        
        public ResponseMessage()
        {

        }

    }
    public class ClientResponse<T> : ResponseMessage<T>, IClientBaseResponse<T>
    {
        public virtual bool IsSuccess { get { return Response.IsSuccessStatusCode; }  } 
        public string Content { get; protected set; }
        public string Message { get; protected set; } = string.Empty;
        public HttpResponseMessage Response { get; private set; }

        public ClientResponse(HttpResponseMessage response, bool isSuccess)
        {
            Response = response;
        }
        public virtual async Task<IClientBaseResponse<T>> Read()
        {
            Content  = await Response.Content.ReadAsStringAsync();
            return this;
        }
        public  ClientResponse<T> DeserializeObject()
        {
            Data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Content);
            return this;
        }
        public void Dispose()
        {
           Response.Dispose();
        }

    }
    internal class ClientCacheResponse<T> : ClientResponse<T>
    {
        public ClientCacheResponse(T data) : base(null, true)
        {
            base.Data = data;
        }
        public override bool IsSuccess => true;
    }
    internal class ClientSuccessResponse<T> : ClientResponse<T> 
    {
        public override async Task<IClientBaseResponse<T>> Read()
        {
            await base.Read();
            base.DeserializeObject();
            return this;
        }

       
        public ClientSuccessResponse(HttpResponseMessage response):base(response,true)
        {
            
        }        
    }
    internal class ClientFailedResponse<T> : ClientResponse<T>
    {        
        public ClientFailedResponse(HttpResponseMessage response):base(response,false)
        {
           
        }
        public override async Task<IClientBaseResponse<T>> Read()
        {
            await base.Read();
            Message = Response.ReasonPhrase;
            Trace.TraceInformation($"{Response.StatusCode}: {Message}");
            return this;
        }
    }
}

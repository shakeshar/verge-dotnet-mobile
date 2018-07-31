using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Verge.Mobile.Contracts
{
    public interface IResponseMessage<T>
    {
        T Data { get; }
    }
    public interface IClientBaseResponse<T> : IResponseMessage<T>, IDisposable
    {
        HttpResponseMessage Response { get; }
        bool IsSuccess { get; }
        string Content { get; }
        string Message { get; }
        Task<IClientBaseResponse<T>> Read();

    }
}

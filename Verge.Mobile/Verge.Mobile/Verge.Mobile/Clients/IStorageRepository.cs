using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Verge.Mobile.Clients
{
    public interface IStorageRepository<T>
         where T : class, new()
    {
        Task Delete();
        Task<bool> Exist();
        Task Save(T content);
        Task<T> Load();
        Task Create();
    }
}

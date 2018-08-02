using System.Collections.Generic;

namespace Verge.Mobile.Services
{
    public interface IStorageService
    {
        IDictionary<string, object> Cache { get; }
        //T Item { get; set; }
        T GetItem<T>(string key) where T : new();
        
        string Key { get; }
    }
}
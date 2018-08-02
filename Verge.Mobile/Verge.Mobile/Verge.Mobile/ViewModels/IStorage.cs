namespace Verge.Mobile.ViewModels
{
    public interface IStorage<T>
    {
        T Get(string key);
    }
}
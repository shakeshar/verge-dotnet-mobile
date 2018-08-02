namespace Verge.Mobile.ViewModels
{
    public interface IRPCCredentials
    {
        string Password { get; set; }
        string Url { get; set; }
        string Username { get; set; }
        int Port { get; set; }
    }
}
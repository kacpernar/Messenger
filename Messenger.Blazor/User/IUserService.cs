namespace Messenger.Blazor.Services;

public interface IUserService
{
    string? UserName { get; set; }
    bool Login(string name, string password);
}
namespace Messenger.Blazor.Services;

public interface IUser
{
    string Name { get; set; }
    string Password { get; set; }
}
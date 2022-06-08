namespace Messenger.Blazor.Services;

public class UserService : IUserService
{
    public string? UserName { get; set; }

    public bool Login(string name, string password)
    {
        var user = UsersStore.Users.FirstOrDefault(t => t.Key == name).Value;
        if (string.IsNullOrEmpty(user) || !user.Equals(password)) return true;
        UserName = name;
        return false;
    }
    
}
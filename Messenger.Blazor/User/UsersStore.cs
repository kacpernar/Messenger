namespace Messenger.Blazor.Services;

public static class UsersStore
{
    public static Dictionary<string, string> Users = new()
    {
        ["Kacper"] = "password",
        ["Natalia"] = "password",
        ["Miki"] = "password",
        ["Jedi"] = "password"
    };
}
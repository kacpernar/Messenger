﻿namespace Messenger.Blazor.Services;

public class User : IUser
{
    public string? Name { get; set; }
    public string? Password { get; set; }
}
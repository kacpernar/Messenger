﻿namespace Messenger;

public class Message : IMessage
{
    public string Text { get; set; }
    public string UserName { get; set; }
    public bool DeleteButtonsVisibility { get; set; }
    public bool DeleteMessage { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
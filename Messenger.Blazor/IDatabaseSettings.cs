namespace Messenger.Blazor
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string MessagesCollectionName { get; set; }
    }
}

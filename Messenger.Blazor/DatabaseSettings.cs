namespace Messenger.Blazor
{
    public class DatabaseSettings :IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string MessagesCollectionName { get; set; }
    }
}

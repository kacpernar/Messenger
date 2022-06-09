namespace Message_Service
{
    public class DatabaseSettings :IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

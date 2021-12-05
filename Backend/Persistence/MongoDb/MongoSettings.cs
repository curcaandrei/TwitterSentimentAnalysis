namespace Persistence.MongoDb
{
    public class MongoSettings
    {
        public string Connection { get; set; }
        public string DatabaseName { get; set; }

        public MongoSettings(string connection , string databaseName)
        {
            Connection = connection;
            DatabaseName = databaseName;
        }

        public MongoSettings()
        {
            
        }
    }
}
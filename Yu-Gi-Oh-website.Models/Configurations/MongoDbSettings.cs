namespace Yu_Gi_Oh_website.Models.Configurations
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionVisitorCount { get; set; } = null!;

    }
}

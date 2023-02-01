namespace FunBooksAndVideos.Infrastructure
{
    public class InfrastructureConfig
    {
        public PersistenceConfig? PersistenceConfig { get; set; }
    }

    public class PersistenceConfig
    {
        public string? ConnectionString { get; set; }
    }
}
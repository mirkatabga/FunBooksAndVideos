namespace FunBooksAndVideos.Domain
{
    public class Video : Product
    {
        public Video(
            Guid id,
            string name,
            string description,
            decimal price,
            int durationSeconds)
            : base(id, name, description, price)
        {
            DurationSeconds = durationSeconds;
        }

        public int DurationSeconds { get; private set; }
    }
}
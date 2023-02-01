namespace FunBooksAndVideos.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" with key {key} was not found.")
        {
        }

        public NotFoundException(string name, object[] keys)
            : base($"\"{name}\" with keys {string.Join(',', keys)} were not found.")
        {
        }
    }
}
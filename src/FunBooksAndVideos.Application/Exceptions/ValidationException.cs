using FluentValidation.Results;

namespace FunBooksAndVideos.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public ValidationException(string property, string error)
        {
            Errors = new Dictionary<string, string[]>
            {
                { property, new string[] { error } }
            };
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
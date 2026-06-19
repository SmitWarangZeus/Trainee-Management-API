namespace TraineeManagement.api.Exceptions;

public class ContentTooLargeException : Exception
{
    public ContentTooLargeException(string message) : base(message) {}
}
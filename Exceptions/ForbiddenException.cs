namespace TraineeManagement.api.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message) {}
}
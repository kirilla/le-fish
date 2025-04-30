namespace Lefish.Common.Exceptions;

public class BlockedByKeyException : Exception
{
    public BlockedByKeyException()
    {
    }

    public BlockedByKeyException(string? message) : base(message)
    {
    }

    public BlockedByKeyException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

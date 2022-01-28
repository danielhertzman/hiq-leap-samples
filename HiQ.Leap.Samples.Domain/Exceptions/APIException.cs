using System.Net;

namespace HiQ.Leap.Samples.Domain.Exceptions;

public abstract class APIException : Exception
{
    protected APIException(string message)
        : base(message)
    {
    }

    public abstract HttpStatusCode StatusCode { get; }
}
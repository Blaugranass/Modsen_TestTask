namespace Library.Application.Exceptions;

public abstract class CustomException(string Message, int statusCode, string errorCode) : Exception(Message)
{
    public int StatusCode = statusCode;
    
    public string ErrorCode = errorCode;
}



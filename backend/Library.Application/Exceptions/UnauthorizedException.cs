using Microsoft.AspNetCore.Http;

namespace Library.Application.Exceptions;

public class UnauthorizedException(string message) : CustomException(message, StatusCodes.Status401Unauthorized, "Unauthorize")
{

}

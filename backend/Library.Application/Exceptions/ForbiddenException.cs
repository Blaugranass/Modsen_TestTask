using Microsoft.AspNetCore.Http;

namespace Library.Application.Exceptions;

public class ForbiddenException(string message) : CustomException(message, StatusCodes.Status403Forbidden, "Forbidden")
{

}

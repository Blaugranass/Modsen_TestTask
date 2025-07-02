using Microsoft.AspNetCore.Http;

namespace Library.Application.Exceptions;

public class NotFoundException(string message) : CustomException(message, StatusCodes.Status404NotFound, "Not Found")
{

}

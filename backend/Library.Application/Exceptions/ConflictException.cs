using Microsoft.AspNetCore.Http;

namespace Library.Application.Exceptions;

public class ConflictException(string message) : CustomException(message, StatusCodes.Status409Conflict, "Conflict")
{

}

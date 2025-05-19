using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.DTOs.PictureDtos;

public record class AddPictureDto
{
    [FromForm(Name = "file")]
    public IFormFile File { get; set; } = null!;
}

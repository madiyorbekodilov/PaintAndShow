using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaintAndShow.Service.DTOs.Photos;
using PaintAndShow.Service.Interfaces;
using PaintAndShow.WebApi.Models;

namespace PaintAndShow.WebApi.Controllers;

public class PhotosController : BaseController
{
    private readonly IPhotoService photoService;
    public PhotosController(IPhotoService photoService)
    {
        this.photoService = photoService;
    }

    [Authorize]
    [HttpPost("UploadPhoto")]
    public async Task<IActionResult> UploadPhoto(PhotoCreationDto path)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = this.photoService.AddAsync(path)
        });
}

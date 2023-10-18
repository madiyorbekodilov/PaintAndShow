using Microsoft.AspNetCore.Mvc;
using PaintAndShow.Service.Interfaces;
using PaintAndShow.WebApi.Models;

namespace PaintAndShow.WebApi.Controllers;

public class AuthController : BaseController
{
    private readonly IAutService authService;
    public AuthController(IAutService authService)
    {
        this.authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> GenerateTokenAsync(string username, string password)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.authService.GenerateTokenAsync(username, password)
        });
}

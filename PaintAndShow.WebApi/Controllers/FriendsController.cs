using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaintAndShow.Service.DTOs.Users;
using PaintAndShow.Service.Interfaces;
using PaintAndShow.WebApi.Models;

namespace PaintAndShow.WebApi.Controllers;

public class FriendsController : BaseController
{
    private readonly IFrieandService frieandService;
    private readonly IUserProvider userProvider;
    public FriendsController(IFrieandService frieandService, IUserProvider userProvider)
    {
        this.frieandService = frieandService;
        this.userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));

    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(string FriendsName,long YourId)
    {
        //var id = long.Parse(this.userProvider.GetUserId());
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.frieandService.AddAsync(FriendsName, YourId)
        });
    }

    [Authorize]
    [HttpDelete("delete")]
    public async ValueTask<IActionResult> DeleteAsync(string UserName, long YourId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.frieandService.RemoveAsync(UserName,YourId)
        });
}

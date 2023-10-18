using Microsoft.AspNetCore.Mvc;
using PaintAndShow.WebApi.Models;
using PaintAndShow.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PaintAndShow.WebApi.Controllers;

public class FriendsController : BaseController
{
    private readonly IFrieandService frieandService;
    private readonly IUserProvider userProvider;
    public FriendsController(IFrieandService frieandService)
    {
        this.frieandService = frieandService;
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(string FriendsName, long YourId)
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
            Data = await this.frieandService.RemoveAsync(UserName, YourId)
        });

    [Authorize]
    [HttpGet("MyFriends")]
    public async Task<IActionResult> GetAllFriends(long YourId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.frieandService.RetrieveAllAsync(YourId)
        });
}

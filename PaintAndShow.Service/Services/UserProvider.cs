using Microsoft.AspNetCore.Http;
using PaintAndShow.Service.Interfaces;
using System.Security.Claims;

namespace PaintAndShow.Service.Services;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _context;

    public UserProvider(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public string GetUserId()
    {
        return _context.HttpContext.User.Claims
                   .First(i => i.Type == ClaimTypes.NameIdentifier).Value;
    }
}

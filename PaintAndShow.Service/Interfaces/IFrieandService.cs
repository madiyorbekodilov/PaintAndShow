using PaintAndShow.Service.DTOs.Friends;
using PaintAndShow.Service.DTOs.Users;

namespace PaintAndShow.Service.Interfaces;

public interface IFrieandService
{
    Task<FriendResultDto> AddAsync(string name, long id);
    Task<bool> RemoveAsync(string username, long id);
    Task<List<UserResultDto>> RetrieveAllAsync(long id);
}

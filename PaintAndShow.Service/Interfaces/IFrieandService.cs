using PaintAndShow.Domain.Entities;
using PaintAndShow.Service.DTOs.Friends;

namespace PaintAndShow.Service.Interfaces;

public interface IFrieandService
{
    Task<FriendResultDto> AddAsync(string name);
    Task<bool> RemoveAsync(long username);
    Task<IEnumerable<User>> RetrieveAllAsync();
}

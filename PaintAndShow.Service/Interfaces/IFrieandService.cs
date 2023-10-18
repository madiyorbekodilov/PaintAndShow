using PaintAndShow.Domain.Entities;
using PaintAndShow.Service.DTOs.Friends;

namespace PaintAndShow.Service.Interfaces;

public interface IFrieandService
{
    Task<FriendResultDto> AddAsync(string name, long id);
    Task<bool> RemoveAsync(string username , long id);
    Task<IEnumerable<User>> RetrieveAllAsync();
}

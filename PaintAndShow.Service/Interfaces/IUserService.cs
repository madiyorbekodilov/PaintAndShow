using PaintAndShow.Service.DTOs.Users;

namespace PaintAndShow.Service.Interfaces;

public interface IUserService
{
    Task<UserResultDto> AddAsync(UserCreationDto dto);
    Task<UserResultDto> ModifyAsync(UserUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<UserResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<UserResultDto>> RetrieveAllAsync();
}

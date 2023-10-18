using PaintAndShow.Service.DTOs.Users;

namespace PaintAndShow.Service.Interfaces;

public interface IUserService
{
    public Task<UserResultDto> AddAsync(UserCreationDto dto);
    Task<UserResultDto> ModifyAsync(UserUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    public Task<UserResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<UserResultDto>> RetrieveAllAsync();
}

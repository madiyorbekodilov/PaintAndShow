using PaintAndShow.Service.DTOs.Photos;

namespace PaintAndShow.Service.Interfaces;

public interface IPhotoService
{
    Task<PhotoResultDto> AddAsync(PhotoCreationDto dto);
    Task<PhotoResultDto> ModifyAsync(PhotoUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<PhotoResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<PhotoResultDto>> RetrieveAllAsync();
}

using PaintAndShow.Service.DTOs.Photos;

namespace PaintAndShow.Service.Interfaces;

public interface IPhotoService
{
    Task<PhotoResultDto> AddAsync(PhotoCreationDto dto);
    Task<bool> RemoveAsync(string photoName);
    Task<PhotoResultDto> RetrieveByIdAsync(string photoName);
    Task<IEnumerable<PhotoResultDto>> RetrieveAllAsync();
}

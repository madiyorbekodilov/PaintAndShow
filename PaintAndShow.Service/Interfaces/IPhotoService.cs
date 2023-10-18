using PaintAndShow.Service.DTOs.Friends;
using PaintAndShow.Service.DTOs.Photos;

namespace PaintAndShow.Service.Interfaces;

public interface IPhotoService
{
    Task<PhotoResultDto> AddAsync(string name, string description ,AttachmentCrDto dto);
    Task<bool> RemoveAsync(string photoName);
    Task<IEnumerable<PhotoResultDto>> RetrieveAllAsync();
}

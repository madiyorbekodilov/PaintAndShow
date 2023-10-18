
using PaintAndShow.Service.DTOs;
using PaintAndShow.Service.DTOs.Friends;
using PaintAndShow.Service.Extentions;
using PaintAndShow.Service.Helpers;

namespace PaintAndShow.Service.Services;

public class AttachmentService
{
    public async ValueTask<Attachment> UploadAsync(AttachmentCrDto dto)
    {
        var webrootPath = Path.Combine(PathHelper.WebRootPath, "Files");

        if (!Directory.Exists(webrootPath))
            Directory.CreateDirectory(webrootPath);

        var fileExtension = Path.GetExtension(dto.formFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtension}";
        var fullPath = Path.Combine(webrootPath, fileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(dto.formFile.ToByte());

        var createdAttachment = new Attachment
        {
            FileName = fileName,
            FilePath = fullPath
        };

        return createdAttachment;
    }
}

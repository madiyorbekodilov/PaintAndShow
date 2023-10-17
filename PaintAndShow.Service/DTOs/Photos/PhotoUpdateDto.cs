namespace PaintAndShow.Service.DTOs.Photos;

public class PhotoUpdateDto
{
    public long Id { get; set; }
    public string PhotoName { get; set; }
    public string? Description { get; set; }
    public string Path { get; set; }
}

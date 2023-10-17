using PaintAndShow.Domain.Commons;

namespace PaintAndShow.Domain.Entities;

public class Photo : Auditable
{
    public string PhotoName { get; set; }
    public string? Description { get; set; }
    public string Path { get; set; }
    public long UserId { get; set; }
}

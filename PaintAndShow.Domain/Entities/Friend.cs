using PaintAndShow.Domain.Commons;

namespace PaintAndShow.Domain.Entities;

public class Friend : Auditable
{
    public long UserId { get; set; }
    public long FriendsId { get; set; }
}

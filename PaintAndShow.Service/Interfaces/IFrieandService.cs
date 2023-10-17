using PaintAndShow.Domain.Entities;

namespace PaintAndShow.Service.Interfaces;

public interface IFrieandService
{
    Task<User> AddAsync(string username);
    Task<bool> RemoveAsync(long username);
    Task<IEnumerable<User>> RetrieveAllAsync();
}

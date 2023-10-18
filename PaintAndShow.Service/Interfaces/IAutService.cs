namespace PaintAndShow.Service.Interfaces;

public interface IAutService
{
    ValueTask<string> GenerateTokenAsync(string phone, string originalPassword);
}

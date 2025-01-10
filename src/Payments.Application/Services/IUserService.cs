namespace Payments.Application.Services;

public interface IUserService
{
    Task<Guid?> GetUserIdAsync();
    string GetToken();
}

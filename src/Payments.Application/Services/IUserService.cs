namespace Payments.Application.Services;

public interface IUserService
{
    Guid? GetUserIdAsync();
    string GetToken();
}

namespace Payments.Application.Services;

public interface IUserService
{
    Guid? GetUserId();
    string? GetUserEmail();
    string GetToken();
}

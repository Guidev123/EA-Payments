using System.Text.RegularExpressions;

namespace Payments.Domain.ValueObjects;

public record Email : ValueObject
{
    public Email(string address)
    {
        if (!IsValid(address)) throw new Exception("Invalid email address");
        Address = address;
    }

    public const int MAX_LENGTH = 254;
    public const int MIN_LENGTH = 5;
    public string Address { get; private set; } = string.Empty;
    private static bool IsValid(string email)   
    {
        if (string.IsNullOrEmpty(email)
            || email.Length < MIN_LENGTH
            || email.Length > MAX_LENGTH) return false;

        email.ToLowerInvariant().Trim();
        const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        return Regex.IsMatch(email, pattern);
    }
}

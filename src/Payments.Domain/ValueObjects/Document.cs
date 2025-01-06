namespace Payments.Domain.ValueObjects;

public record Document : ValueObject
{
    public Document(string number)
    {
        if (!IsValid(number)) throw new Exception("Invalid document number");
        Number = number;
    }
    public const int MAX_LENGTH = 14;
    public const int MIN_LENGTH = 11;
    public string Number { get; private set; } = string.Empty;
    private static bool IsValid(string number)
    {
        if (string.IsNullOrEmpty(number)
            || number.Length < MIN_LENGTH
            || number.Length > MAX_LENGTH) return false;
        number = number.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
        return ValidateCPF(number);
    }
    private static bool ValidateCPF(string number)
    {
        var sum = 0;
        var remainder = 0;
        for (var i = 1; i <= 9; i++)
            sum += int.Parse(number[^(i + 1)].ToString()) * i;
        remainder = sum % 11;
        if (remainder == 10) remainder = 0;
        if (remainder != int.Parse(number[^1].ToString())) return false;
        sum = 0;
        for (var i = 0; i <= 9; i++)
            sum += int.Parse(number[^(i + 1)].ToString()) * i;
        remainder = sum % 11;
        if (remainder == 10) remainder = 0;
        return remainder == int.Parse(number[^0].ToString());
    }
}

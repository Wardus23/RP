using System.Text.RegularExpressions;

public class PhoneNumber
{
    private readonly string number;

    private readonly string ext;

    public string Number => "+" + number;

    public string Ext => ext;

    public PhoneNumber(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Phone number cannot be null or empty", nameof(text));
        }

        string cleaned = Regex.Replace(text, @"[\s\-\(\)]", "");
        string[] parts = cleaned.Split('x', 'X', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
        {
            throw new ArgumentException("Invalid phone number format", nameof(text));
        }

        string mainNumber = parts[0];
        if (mainNumber.StartsWith("+"))
        {
            mainNumber = mainNumber.Substring(1);
        }

        if (!mainNumber.All(char.IsDigit))
        {
            throw new ArgumentException("Phone number can contain only digits after cleaning", nameof(text));
        }

        if (mainNumber.Length < 6)
        {
            throw new ArgumentException("Phone number is too short", nameof(text));
        }

        number = mainNumber;
        ext = parts.Length > 1 ? parts[1] : "";

        if (!string.IsNullOrEmpty(ext) && !ext.All(char.IsDigit))
        {
            throw new ArgumentException("Extension can contain only digits", nameof(text));
        }
    }

    public override string ToString()
    {
        return string.IsNullOrEmpty(ext)
            ? $"+{number}"
            : $"+{number}x{ext}";
    }

    public override bool Equals(object obj)
    {
        return obj is PhoneNumber other &&
               number == other.number &&
               ext == other.ext;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(number, ext);
    }
}
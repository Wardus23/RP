using ModelLib.Tests;

public class PhoneNumberTests
{
    [Theory]
    [MemberData(nameof(ValidPhoneNumberTestData))]
    public void Can_create_number_with_valid_arguments(string input, string expectedNumber, string expectedExt)
    {
        PhoneNumber phoneNumber = new(input);
        Assert.Equal(expectedNumber, phoneNumber.Number);
        Assert.Equal(expectedExt, phoneNumber.Ext);
    }

    [Theory]
    [MemberData(nameof(InvalidPhoneNumberTestData))]
    public void Cannot_create_number_with_invalid_arguments(string input)
    {
        Assert.Throws<ArgumentException>(() => new PhoneNumber(input));
    }

    [Theory]
    [MemberData(nameof(ToStringTestData))]
    public void Can_to_string_return_right_value(string input, string expected)
    {
        PhoneNumber phoneNumber = new PhoneNumber(input);
        string result = phoneNumber.ToString();
        Assert.Equal(expected, result);
    }

    public static TheoryData<string, string, string> ValidPhoneNumberTestData()
    {
        return new TheoryData<string, string, string>
        {
            { "+7 (8362) 45-02-72", "+78362450272", "" },
            { "7 (8362) 45-02-72", "+78362450272", "" },
            { "8 (8362) 45-02-72", "+88362450272", "" },
            { "1-234-567-8901 x1234", "+12345678901", "1234" },
            { "1234567890x999", "+1234567890", "999" },
            { "+1 (234) 567-8901 x12", "+12345678901", "12" },
        };
    }

    public static TheoryData<string> InvalidPhoneNumberTestData()
    {
        return new TheoryData<string>
        {
            "",
            "   ",
            "abc",
            "123",
            "12-abc-34",
            "123xabc",
            "+",
            "x123",
            "123x",
            "++123",
        };
    }

    public static TheoryData<string, string> ToStringTestData()
    {
        return new TheoryData<string, string>
        {
            { "+7 (8362) 45-02-72", "+78362450272" },
            { "1-234-567-8901 x1234", "+12345678901x1234" },
            { "1234567890", "+1234567890" },
        };
    }
}
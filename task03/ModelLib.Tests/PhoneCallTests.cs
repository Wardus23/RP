namespace ModelLib.Tests;

public class PhoneCallTests
{
    private readonly PhoneNumber caller = new PhoneNumber("+1234567890");
    private readonly PhoneNumber callee = new PhoneNumber("+0987654321");
    private readonly DateTime start = new DateTime(2024, 1, 1, 10, 0, 0);

    [Fact]
    public void Can_create_failed_with_valid_arguments()
    {
        PhoneCall call = PhoneCall.CreateFailed(start, caller, callee);
        Assert.Equal(PhoneCallStatus.Failed, call.Status);
        Assert.Equal(start, call.Start);
        Assert.Equal(start, call.End);
        Assert.Equal(TimeSpan.Zero, call.DialUpDuration);
        Assert.Equal(TimeSpan.Zero, call.TalkDuration);
        Assert.Equal(TimeSpan.Zero, call.TotalDuration);
    }

    [Fact]
    public void Cannot_create_failed_with_same_arguments()
    {
        Assert.Throws<ArgumentException>(() => PhoneCall.CreateFailed(start, caller, caller));
    }

    [Fact]
    public void Can_create_missed_with_valid_arguments()
    {
        DateTime end = start.AddMinutes(1);
        PhoneCall call = PhoneCall.CreateMissed(start, end, caller, callee);
        Assert.Equal(PhoneCallStatus.Missed, call.Status);
        Assert.Equal(start, call.Start);
        Assert.Equal(end, call.End);
        Assert.Equal(TimeSpan.FromMinutes(1), call.DialUpDuration);
        Assert.Equal(TimeSpan.Zero, call.TalkDuration);
        Assert.Equal(TimeSpan.FromMinutes(1), call.TotalDuration);
    }

    [Fact]
    public void Cannot_create_missed_with_end_before_start()
    {
        DateTime end = start.AddMinutes(-1);
        Assert.Throws<ArgumentException>(() => PhoneCall.CreateMissed(start, end, caller, callee));
    }

    [Fact]
    public void Can_create_accepted_with_valid_arguments()
    {
        DateTime acceptedAt = start.AddSeconds(15);
        DateTime end = start.AddMinutes(2);
        PhoneCall call = PhoneCall.CreateAccepted(start, acceptedAt, end, caller, callee);
        Assert.Equal(PhoneCallStatus.Accepted, call.Status);
        Assert.Equal(start, call.Start);
        Assert.Equal(end, call.End);
        Assert.Equal(TimeSpan.FromSeconds(15), call.DialUpDuration);
        Assert.Equal(TimeSpan.FromMinutes(1.75), call.TalkDuration);
        Assert.Equal(TimeSpan.FromMinutes(2), call.TotalDuration);
    }

    [Fact]
    public void Cannot_create_accepted_with_zero_talk_duration()
    {
        DateTime acceptedAt = start.AddMinutes(1);
        DateTime end = acceptedAt;
        Assert.Throws<ArgumentException>(() => PhoneCall.CreateAccepted(start, acceptedAt, end, caller, callee));
    }

    [Theory]
    [MemberData(nameof(InvalidDurationTestData))]
    public void CreateMethods_WithInvalidDurations_ThrowException(
        Func<PhoneCall> createMethod)
    {
        Assert.Throws<ArgumentException>(createMethod);
    }

    public static TheoryData<Func<PhoneCall>> InvalidDurationTestData()
    {
        PhoneNumber caller = new PhoneNumber("+1234567890");
        PhoneNumber callee = new PhoneNumber("+0987654321");
        DateTime start = new DateTime(2024, 1, 1, 10, 0, 0);
        DateTime end = start.AddMinutes(1);
        DateTime acceptedAt = start.AddSeconds(30);

        return new TheoryData<Func<PhoneCall>>
        {
            () => PhoneCall.CreateMissed(start, start.AddMinutes(-1), caller, callee),
            () => PhoneCall.CreateAccepted(start, start.AddMinutes(2), end, caller, callee),
            () => PhoneCall.CreateAccepted(start, start, start, caller, callee),
        };
    }
}
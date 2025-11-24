public class PhoneCall
{
    public PhoneCallStatus Status { get; }

    public DateTime Start { get; }

    public TimeSpan DialUpDuration { get; }

    public TimeSpan TalkDuration { get; }

    public TimeSpan TotalDuration { get; }

    public DateTime End { get; }

    public PhoneNumber Caller { get; }

    public PhoneNumber Callee { get; }

    private PhoneCall(PhoneCallStatus status, DateTime start, DateTime end, TimeSpan dialUpDuration, TimeSpan talkDuration, PhoneNumber caller, PhoneNumber callee)
    {
        ValidateInvariants(caller, callee, start, end, dialUpDuration, talkDuration, status);
        Status = status;
        Start = start;
        End = end;
        DialUpDuration = dialUpDuration;
        TalkDuration = talkDuration;
        TotalDuration = end - start;
        Caller = caller;
        Callee = callee;
    }

    public static PhoneCall CreateFailed(DateTime start, PhoneNumber caller, PhoneNumber callee)
    {
        return new PhoneCall(
            PhoneCallStatus.Failed,
            start,
            start,
            TimeSpan.Zero,
            TimeSpan.Zero,
            caller,
            callee);
    }

    public static PhoneCall CreateMissed(DateTime start, DateTime end, PhoneNumber caller, PhoneNumber callee)
    {
        TimeSpan totalDuration = end - start;
        return new PhoneCall(
            PhoneCallStatus.Missed,
            start,
            end,
            totalDuration,
            TimeSpan.Zero,
            caller,
            callee);
    }

    public static PhoneCall CreateDeclined(DateTime start, DateTime end, PhoneNumber caller, PhoneNumber callee)
    {
        TimeSpan totalDuration = end - start;
        return new PhoneCall(
            PhoneCallStatus.Declined,
            start,
            end,
            totalDuration,
            TimeSpan.Zero,
            caller,
            callee);
    }

    public static PhoneCall CreateAccepted(DateTime start, DateTime acceptedAt, DateTime end, PhoneNumber caller, PhoneNumber callee)
    {
        TimeSpan dialUpDuration = acceptedAt - start;
        TimeSpan talkDuration = end - acceptedAt;
        return new PhoneCall(
            PhoneCallStatus.Accepted,
            start,
            end,
            dialUpDuration,
            talkDuration,
            caller,
            callee);
    }

    private static void ValidateInvariants(PhoneNumber caller, PhoneNumber callee, DateTime start, DateTime end, TimeSpan dialUpDuration, TimeSpan talkDuration, PhoneCallStatus status)
    {
        if (caller.Equals(callee))
        {
            throw new ArgumentException("Caller and callee cannot be the same");
        }

        if (end < start)
        {
            throw new ArgumentException("End time cannot be less than start time");
        }

        if (dialUpDuration < TimeSpan.Zero)
        {
            throw new ArgumentException("Dial-up duration cannot be negative");
        }

        if (talkDuration < TimeSpan.Zero)
        {
            throw new ArgumentException("Talk duration cannot be negative");
        }

        TimeSpan calculatedTotal = end - start;
        if (calculatedTotal < TimeSpan.Zero)
        {
            throw new ArgumentException("Total duration cannot be negative");
        }

        TimeSpan sumDurations = dialUpDuration + talkDuration;
        if (Math.Abs(calculatedTotal.TotalSeconds - sumDurations.TotalSeconds) > 0.001)
        {
            throw new ArgumentException("Total duration must equal dial-up duration plus talk duration");
        }

        switch (status)
        {
            case PhoneCallStatus.Failed:
                if (dialUpDuration != TimeSpan.Zero)
                {
                    throw new ArgumentException("Failed call must have zero dial-up duration");
                }

                if (talkDuration != TimeSpan.Zero)
                {
                    throw new ArgumentException("Failed call must have zero talk duration");
                }

                break;
            case PhoneCallStatus.Missed:
            case PhoneCallStatus.Declined:
                if (talkDuration != TimeSpan.Zero)
                {
                    throw new ArgumentException($"Declined call must have zero talk duration");
                }

                break;
            case PhoneCallStatus.Accepted:
                if (talkDuration <= TimeSpan.Zero)
                {
                    throw new ArgumentException("Accepted call must have positive talk duration");
                }

                break;
        }
    }
}
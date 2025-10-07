namespace DomeGym.UnitTests.Utils;

public class FakeTimeProvider : TimeProvider
{
    public override DateTimeOffset GetUtcNow()
    {
        var date = Constants.Constants.Sessions.Date;

        return new DateTimeOffset(date, TimeOnly.MinValue, TimeSpan.Zero);
    }
}
namespace DomeGym.UnitTests.Constants;

public static partial class Constants
{
    public static class Sessions
    {
        public static Guid Id = Guid.NewGuid();
        public static DateOnly Date = DateOnly.FromDateTime(DateTime.UtcNow);
        public static TimeOnly StartTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(3));
        public static TimeOnly EndTime = StartTime.AddHours(3);
    }
}
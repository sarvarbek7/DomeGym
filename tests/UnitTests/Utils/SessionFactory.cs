using DomeGym.Domain;

namespace DomeGym.UnitTests.Utils;

public static class SessionFactory
{
    public static Session Create(ushort maxNumberOfParticipants, DateOnly? date = null, TimeOnly? startTime = null,
        TimeOnly? endTime = null, Guid? id = null)
    {
        return new Session(maxNumberOfParticipants,
            date ?? Constants.Constants.Sessions.Date,
            startTime ?? Constants.Constants.Sessions.StartTime,
            endTime ??  Constants.Constants.Sessions.EndTime,
            id ?? Constants.Constants.Sessions.Id);
    }
}
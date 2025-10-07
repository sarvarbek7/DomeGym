using DomeGym.Domain;

namespace DomeGym.UnitTests.Utils;

public static class SessionFactory
{
    public static Session CreateSession(ushort maxNumberOfParticipants, Guid? id = null)
    {
        return new Session(maxNumberOfParticipants, id ?? Constants.Constants.Sessions.Id);
    }
}
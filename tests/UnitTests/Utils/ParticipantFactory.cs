using DomeGym.Domain;

namespace DomeGym.UnitTests.Utils;

public static class ParticipantFactory
{
    public static Participant Create(Guid? id = null)
    {
        return new Participant(id ??  Constants.Constants.Participants.Id);
    }
}
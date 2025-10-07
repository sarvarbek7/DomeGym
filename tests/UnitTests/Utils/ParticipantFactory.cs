using DomeGym.Domain;

namespace DomeGym.UnitTests.Utils;

public static class ParticipantFactory
{
    public static Participant CreateParticipant(Guid? id = null)
    {
        return new Participant(id ??  Constants.Constants.Participants.Id);
    }
}
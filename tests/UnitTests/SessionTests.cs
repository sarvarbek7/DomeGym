using DomeGym.Domain;
using DomeGym.UnitTests.Utils;

namespace DomeGym.UnitTests;

public class SessionTests
{
    [Fact]
    public void SessionReturnsErrorWhenNoRoomForParticipant()
    {
        // ARRANGE
        var session = SessionFactory.CreateSession(maxNumberOfParticipants: 1);
        var participant1 = ParticipantFactory.CreateParticipant(Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(Guid.NewGuid());

        // ACT
        var participant1ReserveResult = session.ReserveSpot(participant1);
        var participant2ReserveResult = session.ReserveSpot(participant2);

        // ASSERT
        Assert.False(participant1ReserveResult.IsError);
        Assert.True(participant2ReserveResult.IsError);
        Assert.Equal(participant2ReserveResult.FirstError, SessionErrors.CanNotReserveNoFreeSpot);
    }
}
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

    [Fact]
    public void CanNotCancelTooCloseToSession()
    {
        // ARRANGE
        var date =  DateOnly.FromDateTime(DateTime.UtcNow);
        var startTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(3));
        var endTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(5));
        var participant = ParticipantFactory.CreateParticipant(Guid.NewGuid());
        
        var session = SessionFactory.CreateSession(maxNumberOfParticipants: 2, date, startTime, endTime);
        
        // ACT
        var reserveSpotResult = session.ReserveSpot(participant);
        var cancelReservationResult = session.CancelReservation(participant, new FakeTimeProvider());
        
        // ASSERT
        Assert.False(reserveSpotResult.IsError);
        Assert.True(cancelReservationResult.IsError);
        Assert.Equal(cancelReservationResult.FirstError, SessionErrors.CanNotCancelTooCloseToSession);
    }
    
    [Fact]
    public void CanNotCancelSessionReservationNotFound()
    {
        // ARRANGE
        var date =  DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3));
        var startTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(3));
        var endTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(5));
        var participant1 = ParticipantFactory.CreateParticipant(Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(Guid.NewGuid());
        
        var session = SessionFactory.CreateSession(maxNumberOfParticipants: 2, date, startTime, endTime);
        
        // ACT
        var reserveSpotResult = session.ReserveSpot(participant1);
        var cancelReservationParticipant1Result = session.CancelReservation(participant1);
        var cancelReservationParticipant2Result = session.CancelReservation(participant2);
        
        // ASSERT
        Assert.False(reserveSpotResult.IsError);
        Assert.False(cancelReservationParticipant1Result.IsError);
        Assert.True(cancelReservationParticipant2Result.IsError);
        Assert.Equal(cancelReservationParticipant2Result.FirstError, SessionErrors.ReservationNotFound);
    }
}
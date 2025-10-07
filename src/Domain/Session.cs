using ErrorOr;

namespace DomeGym.Domain;

public class Session
{
    private const int CancellationAllowedHours = 24;
    
    private readonly Guid _id;
    private readonly ushort _maxNumberOfParticipants;
    private readonly List<Guid> _participants = [];
    private readonly DateOnly date;
    private readonly TimeOnly startTime;
    private readonly TimeOnly endTime;
    

    public Session(ushort maxNumberOfParticipants, 
            DateOnly date, 
            TimeOnly startTime, 
            TimeOnly endTime, 
            Guid? id = null)
    {
        _id = id  ?? Guid.NewGuid();
        _maxNumberOfParticipants = maxNumberOfParticipants;
        this.date = date;
        this.startTime = startTime;
        this.endTime = endTime;
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_participants.Count >= _maxNumberOfParticipants)
        {
            return SessionErrors.CanNotReserveNoFreeSpot;
        }
        
        _participants.Add(participant.Id);
        
        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation(Participant participant,
        TimeProvider? timeProvider = null)
    {
        timeProvider ??= TimeProvider.System;
        
        var now = timeProvider.GetUtcNow();
        var start = new DateTime(this.date, this.startTime);

        var remainingTime = start - now;
        
        if (remainingTime < TimeSpan.FromHours(CancellationAllowedHours))
        {
            return SessionErrors.CanNotCancelTooCloseToSession;
        }

        if (!_participants.Remove(participant.Id))
        {
            return SessionErrors.ReservationNotFound;
        }
        
        return Result.Success;
    }
}
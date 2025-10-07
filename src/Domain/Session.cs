using ErrorOr;

namespace DomeGym.Domain;

public class Session
{
    private readonly Guid _id;
    private readonly ushort _maxNumberOfParticipants;
    private readonly List<Guid> _participants = [];

    public Session(ushort maxNumberOfParticipants, Guid? id = null)
    {
        _id = id  ?? Guid.NewGuid();
        _maxNumberOfParticipants = maxNumberOfParticipants;
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
}
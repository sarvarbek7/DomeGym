namespace DomeGym.Domain;

public class Participant
{
    public Guid Id { get; }

    public Participant(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
    }
}
namespace DomeGym.Domain;

public class Gym
{
    public Guid Id { get; }

    public Gym(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
    }
}
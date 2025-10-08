using ErrorOr;

namespace DomeGym.Domain;

public class Subscription
{
    private readonly SubscriptionType _subscriptionType;
    private readonly List<Guid> _gymIds = [];
    
    public Guid Id { get; }
    
    public Subscription(SubscriptionType subscriptionType, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        _subscriptionType = subscriptionType;
    }

    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (_gymIds.Count >= _subscriptionType.MaxNumberOfAllowedGyms)
        {
            return SubscriptionErrors.GymLimitExceedSubscriptionLimit;
        }

        if (_gymIds.Contains(gym.Id))
        {
            return SubscriptionErrors.GymAlreadyAddedToSubscription;
        }
        
        _gymIds.Add(gym.Id);
        
        return Result.Success;
    }
}
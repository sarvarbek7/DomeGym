using DomeGym.Domain;

namespace DomeGym.UnitTests.Utils;

public static class SubscriptionFactory
{
    public static Subscription Create(SubscriptionType subscriptionType, Guid? id = null)
    {
        return new Subscription(subscriptionType, id);
    }
}
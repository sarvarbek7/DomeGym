using System.Collections;
using DomeGym.Domain;
using DomeGym.UnitTests.Utils;
using ErrorOr;

namespace DomeGym.UnitTests;

public class SubscriptionTests
{
    [Theory]
    [ClassData(typeof(SubscriptionTestData))]
    public void SubscriptionAddGymReturnsGymLimitExceedSubscriptionLimitErrorIfLimitExceeded(SubscriptionType subscriptionType, int generatedGymsCount)
    {
        // ARRANGE
        var subscription = SubscriptionFactory.Create(subscriptionType);

        var gyms = Enumerable.Range(1, generatedGymsCount)
            .Select(x => GymFactory.Create(Guid.NewGuid()));

        // ACT
        List<ErrorOr<Success>> addGymsResults = [];
        
        foreach (var gym in gyms)
        {
            addGymsResults.Add(subscription.AddGym(gym));
        }
        
        var lastResult = addGymsResults.Last();
        
        addGymsResults.Remove(lastResult);
        
        var otherResults = addGymsResults;
        
        // ASSERT
        Assert.True(otherResults.All(x => !x.IsError));
        
        Assert.True(lastResult.IsError);
        Assert.Equal(lastResult.FirstError, SubscriptionErrors.GymLimitExceedSubscriptionLimit);
    }

    [Fact]
    public void SubscriptionAddGymReturnsErrorGymAlreadyAddedToSubscription()
    {
        // ARRANGE
        var subscription = SubscriptionFactory.Create(SubscriptionType.Pro);

        var gymId = Guid.NewGuid();
        
        var gym1 =  GymFactory.Create(gymId);
        var gym2 = GymFactory.Create(gymId);
        
        // ACT
        var result1 =  subscription.AddGym(gym1);
        var result2 =  subscription.AddGym(gym2);
        
        // ASSERT
        Assert.False(result1.IsError);
        Assert.True(result2.IsError);
        Assert.Equal(result2.FirstError, SubscriptionErrors.GymAlreadyAddedToSubscription);
    }
}

public class SubscriptionTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { SubscriptionType.Free, SubscriptionType.Free.MaxNumberOfAllowedGyms + 1 };
        yield return new object[] { SubscriptionType.Starter, SubscriptionType.Starter.MaxNumberOfAllowedGyms + 1 };
        yield return new object[] { SubscriptionType.Pro, SubscriptionType.Pro.MaxNumberOfAllowedGyms + 1 };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

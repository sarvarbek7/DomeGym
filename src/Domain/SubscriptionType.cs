using Ardalis.SmartEnum;

namespace DomeGym.Domain;

public abstract class SubscriptionType(string name, int value) : SmartEnum<SubscriptionType>(name, value)
{
    public static readonly SubscriptionType Free = new FreeSubscriptionType(nameof(Free), 1);
    public static readonly SubscriptionType Starter = new StarterSubscriptionType(nameof(Starter), 2);
    public static readonly SubscriptionType Pro = new ProSubscriptionType(nameof(Pro), 3);
    
    public static readonly IReadOnlyList<SubscriptionType> All = [Free, Starter, Pro];
    
    public abstract int MaxNumberOfAllowedGyms { get; }
    public abstract int MaxNumberOfAllowedRoomsPerGym { get; }
    public abstract int MaxNumberOfDailyAllowedSessions { get; }
    public abstract decimal Price { get; }
    
    public bool IsUnlimitedRooms => MaxNumberOfAllowedRoomsPerGym == int.MaxValue;
    public bool IsUnlimitedSessions => MaxNumberOfDailyAllowedSessions == int.MaxValue;
}

public sealed class FreeSubscriptionType(string name, int value) : SubscriptionType(name, value)
{
    public override int MaxNumberOfAllowedGyms => 1;
    public override int MaxNumberOfAllowedRoomsPerGym => 1;
    public override int MaxNumberOfDailyAllowedSessions => 4;
    public override decimal Price => decimal.Zero;
}

public sealed class StarterSubscriptionType(string name, int value) : SubscriptionType(name, value)
{
    public override int MaxNumberOfAllowedGyms => 1;
    public override int MaxNumberOfAllowedRoomsPerGym => 3;
    public override int MaxNumberOfDailyAllowedSessions => int.MaxValue;
    public override decimal Price => 19.99M;
}

public sealed class ProSubscriptionType(string name, int value) : SubscriptionType(name, value)
{
    public override int MaxNumberOfAllowedGyms => 3;
    public override int MaxNumberOfAllowedRoomsPerGym => int.MaxValue;
    public override int MaxNumberOfDailyAllowedSessions => int.MaxValue;
    public override decimal Price => 24.99M;
}
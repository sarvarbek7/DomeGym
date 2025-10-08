using ErrorOr;

namespace DomeGym.Domain;

public static class SubscriptionErrors
{
    public static class Codes
    {
        public const string Prefix = "SubscriptionErrors";
        
        public const string GymLimitExceedSubscriptionLimit =  $"{Prefix}.GymLimitExceedSubscriptionLimit";
        public const string GymAlreadyAddedToSubscription =  $"{Prefix}.GymAlreadyAddedToSubscription";
    }

    public static Error GymLimitExceedSubscriptionLimit => Error.Validation(code: Codes.GymLimitExceedSubscriptionLimit,
        description: "Gym limit exceeds subscription limit.");
    
    public static Error GymAlreadyAddedToSubscription => Error.Validation(code: Codes.GymAlreadyAddedToSubscription, 
        description: "Gym already added to subscription.");
}
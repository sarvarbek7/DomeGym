using ErrorOr;

namespace DomeGym.Domain;

public static class SessionErrors
{
    public static class Codes
    {
        private const string Prefix = "SessionErrors";
        
        public const string CanNotReserveNoFreeSpot =  $"{Prefix}.CanNotReserveSpotNoRoom";
        public const string CanNotCancelTooCloseToSession = $"{Prefix}.CanNotCancelTooCloseToSession";
        public const string ReservationNotFound = $"{Prefix}.ReservationNotFound";
        
    }
    
    public static Error CanNotReserveNoFreeSpot => Error.Validation(code: Codes.CanNotReserveNoFreeSpot,
        description: "Can not reserve spot, maximum number of participants is reached");
    
    public static Error CanNotCancelTooCloseToSession => Error.Validation(code: Codes.CanNotCancelTooCloseToSession,
        description: "Can not cancel session. It is too close to start time.");
    
    public static Error ReservationNotFound => Error.NotFound(code: Codes.ReservationNotFound, 
        description: "Reservation not found");
}
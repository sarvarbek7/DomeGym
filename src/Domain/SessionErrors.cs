using ErrorOr;

namespace DomeGym.Domain;

public static class SessionErrors
{
    public static class Codes
    {
        public const string CanNotReserveNoFreeSpot =  "SessionErrors.CanNotReserveSpotNoRoom";
        public const string ReservationNotFound = "SessionErrors.ReservationNotFound";
    }
    
    public static Error CanNotReserveNoFreeSpot => Error.Validation(code: SessionErrors.Codes.CanNotReserveNoFreeSpot,
        description: "Can not reserve spot, maximum number of participants is reached");
    
    public static Error ReservationNotFound => Error.Validation(code: SessionErrors.Codes.ReservationNotFound, 
        description: "Reservation not found");
}
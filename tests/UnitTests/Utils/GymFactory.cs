using DomeGym.Domain;

namespace DomeGym.UnitTests.Utils;

public static class GymFactory
{
    public static Gym Create(Guid? id = null)
    {
        return new Gym(id ?? Constants.Constants.Gyms.Id);
    }
}
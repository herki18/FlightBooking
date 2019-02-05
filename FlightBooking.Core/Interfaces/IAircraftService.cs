using System.Collections.Generic;

namespace FlightBooking.Core.Interfaces
{
    public interface IAircraftService
    {
        void AddPlanes(params Plane[] planes);
        IEnumerable<Plane> FindAlternativeAircraft(int seats);
        Plane GetPlaneById(int id);
    }
}

using System.Collections.Generic;
using System.Linq;
using FlightBooking.Core.Interfaces;

namespace FlightBooking.Core.Services
{
    public class AircraftService : IAircraftService
    {
        public List<Plane> Planes;

        public AircraftService()
        {
            Planes = new List<Plane>();
        }
        public void AddPlanes(params Plane[] planes)
        {
            Planes.AddRange(planes);
        }

        public IEnumerable<Plane> FindAlternativeAircraft(int seats)
        {
            return Planes.Where(x => x.NumberOfSeats >= seats);
        }

        public Plane GetPlaneById(int id)
        {
            return Planes.SingleOrDefault(x => x.Id == id);
        }
    }
}

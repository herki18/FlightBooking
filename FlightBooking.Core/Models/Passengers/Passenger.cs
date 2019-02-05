using FlightBooking.Core.Interfaces;

namespace FlightBooking.Core.Models.Passengers
{
    public abstract class Passenger
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int AllowedBags { get; set; }

        protected Passenger(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public virtual void Calculate(IFlightService flightService)
        {
            flightService.TotalExpectedBaggage += AllowedBags;
            flightService.CostOfFlight += flightService.FlightRoute.BaseCost;
            flightService.SeatsTaken += 1;
        }
    }
}

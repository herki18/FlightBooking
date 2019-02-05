using FlightBooking.Core.Interfaces;

namespace FlightBooking.Core.Models.Passengers
{
    public class DiscountedPassenger : Passenger
    {
        public DiscountedPassenger(string name, int age) : base(name, age)
        {
            AllowedBags = 0;
        }

        public override void Calculate(IFlightService flightService)
        {
            flightService.ProfitFromFlight += flightService.FlightRoute.BasePrice / 2;
            base.Calculate(flightService);
        }
    }
}

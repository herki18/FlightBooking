using FlightBooking.Core.Interfaces;

namespace FlightBooking.Core.Models.Passengers
{
    public class GeneralPassenger : Passenger
    {
        public GeneralPassenger(string name, int age) : base(name, age)
        {
            AllowedBags = 1;
        }

        public override void Calculate(IFlightService flightService)
        {
            flightService.ProfitFromFlight += flightService.FlightRoute.BasePrice;
            base.Calculate(flightService);
        }
    }
}

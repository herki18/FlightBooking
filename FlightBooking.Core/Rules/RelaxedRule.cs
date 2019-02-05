using FlightBooking.Core.Interfaces;
using FlightBooking.Core.Models.Passengers;

namespace FlightBooking.Core.Rules
{
    public class RelaxedRule : IRule
    {
        public bool CanFlightProceed(IFlightService flightService, IPassengerService passengerService)
        {
            var airlineEmployeesPercentage = passengerService.GetCountForPassengerType<AirlineEmployeePassenger>() / (double)flightService.Aircraft.NumberOfSeats;
            if ((airlineEmployeesPercentage > flightService.FlightRoute.MinimumTakeOffPercentage || flightService.ProfitSurplus > 0)
                && flightService.SeatsTaken / (double)flightService.Aircraft.NumberOfSeats > flightService.FlightRoute.MinimumTakeOffPercentage
                && flightService.SeatsTaken <= flightService.Aircraft.NumberOfSeats
                )
            {
                return true;
            }

            return false;
        }
    }
}

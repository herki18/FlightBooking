using FlightBooking.Core.Interfaces;

namespace FlightBooking.Core.Rules
{
    public class ProfitRule : IRule
    {
        public bool CanFlightProceed(IFlightService flightService, IPassengerService passengerService)
        {
            if (flightService.ProfitSurplus > 0 
                && flightService.SeatsTaken <= flightService.Aircraft.NumberOfSeats 
                && flightService.SeatsTaken / (double)flightService.Aircraft.NumberOfSeats> flightService.FlightRoute.MinimumTakeOffPercentage)
            {
                return true;
            }

            return false;
        }
    }
}

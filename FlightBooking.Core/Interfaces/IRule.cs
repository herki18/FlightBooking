namespace FlightBooking.Core.Interfaces
{
    public interface IRule
    {
        bool CanFlightProceed(IFlightService flightService, IPassengerService passengerService);
    }
}
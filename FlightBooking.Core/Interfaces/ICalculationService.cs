namespace FlightBooking.Core.Interfaces
{
    public interface ICalculationService
    {
        void AddRules(params IRule[] rules);
        void SetCurrentRule<T>() where T : IRule;
        void Calculate(IPassengerService passengers, IFlightService flightService);
    }
}

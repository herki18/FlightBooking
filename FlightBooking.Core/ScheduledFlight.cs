using FlightBooking.Core.Interfaces;
using FlightBooking.Core.Models.Passengers;

namespace FlightBooking.Core
{
    public class ScheduledFlight
    {
        private readonly IFlightService _flightService;
        private readonly ICalculationService _calculationService;
        private readonly IFormat _format;
        private readonly IPassengerService _passengerService;
        private readonly IAircraftService _aircraftService;

        public ScheduledFlight(IFlightService flightService, 
                        IPassengerService passengerService,
                        IAircraftService aircraftService,
                        ICalculationService calculationService, 
                        IFormat format)
        {
            _flightService = flightService;
            _calculationService = calculationService;
            _format = format;
            _passengerService = passengerService;
            _aircraftService = aircraftService;
        }
        
        public void AddPassenger(Passenger passenger)
        {
            _passengerService.AddPassenger(passenger);
        }
        
        public void SetAircraftForRoute(int id)
        {
            _flightService.AddAircraft(_aircraftService.GetPlaneById(id));
        }

        public void SetCurrentRule<T>() where T : IRule
        {
            _calculationService.SetCurrentRule<T>();
        }

        public void Process()
        {
            _flightService.Reset();
            _calculationService.Calculate(_passengerService, _flightService);
        }
        
        public string GetSummary()
        {
            return _format.Format(_flightService);
        }
    }
}

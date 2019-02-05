using System.Collections.Generic;
using System.Linq;
using FlightBooking.Core.Interfaces;

namespace FlightBooking.Core.Services
{
    public class CalculationService : ICalculationService
    {
        public List<IRule> Rules;
        public IRule CurrentRule;

        public CalculationService()
        {
            Rules = new List<IRule>();
        }

        public void AddRules(params IRule[] rules)
        {
            Rules.AddRange(rules);
        }

        public void SetCurrentRule<T>() where T : IRule
        {
            CurrentRule = Rules.OfType<T>().FirstOrDefault();
        }

        public void Calculate(IPassengerService passengers, IFlightService flightService)
        {
            passengers.Calculate(flightService);
            flightService.ProfitSurplus = flightService.ProfitFromFlight - flightService.CostOfFlight;

            flightService.CanFlightProceed = CurrentRule.CanFlightProceed(flightService, passengers);
        }
    }
}

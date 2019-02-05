using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlightBooking.Core.Interfaces;
using FlightBooking.Core.Models.Passengers;

namespace FlightBooking.Core.Formats
{
    public class SummaryFormat : IFormat
    {
        private readonly IPassengerService _passengerService;
        private readonly IAircraftService _aircraftService;
        private readonly string _verticalWhiteSpace = Environment.NewLine + Environment.NewLine;
        private readonly string _newLine = Environment.NewLine;
        private const string Indentation = "    ";
        private readonly Dictionary<string, string> _translation;

        public SummaryFormat(IPassengerService passengerService, IAircraftService aircraftService)
        {
            _passengerService = passengerService;
            _aircraftService = aircraftService;
            _translation = new Dictionary<string, string>
            {
                {nameof(GeneralPassenger), "General sales: "},
                {nameof(LoyaltyPassenger), "Loyalty member sales: "},
                {nameof(DiscountedPassenger), "Discounted sales: "},
                {nameof(AirlineEmployeePassenger), "Airline employee comps: "}
            };
        }

        public string Format(IFlightService flightService)
        {
            StringBuilder result = new StringBuilder();
            result.Append($"Flight summary for {flightService.FlightRoute.Title}");

            result.Append(_verticalWhiteSpace);

            result.Append($"Total passengers: {flightService.SeatsTaken}");
            
            foreach (var passenger in _passengerService.GetPassengersTypes())
            {
                result.Append(_newLine);
                result.Append($"{Indentation}{_translation[passenger.Name]}{_passengerService.GetCountForPassengerType(passenger)}");
            }
            
            result.Append(_verticalWhiteSpace);
            result.Append($"Total expected baggage: {flightService.TotalExpectedBaggage}");

            result.Append(_verticalWhiteSpace);

            result.Append($"Total revenue from flight: {flightService.ProfitFromFlight}");
            result.Append(_newLine);
            result.Append($"Total costs from flight: {flightService.CostOfFlight}");
            result.Append(_newLine);

            result.Append((flightService.ProfitSurplus > 0 ? "Flight generating profit of: " : "Flight losing money of: ") + flightService.ProfitSurplus);

            result.Append(_verticalWhiteSpace);

            result.Append($"Total loyalty points given away: {flightService.TotalLoyaltyPointsAccrued}{_newLine}");
            result.Append($"Total loyalty points redeemed: {flightService.TotalLoyaltyPointsRedeemed}{_newLine}");

            result.Append(_verticalWhiteSpace);

            if (flightService.CanFlightProceed)
            {
                result.Append("THIS FLIGHT MAY PROCEED");
            }
            else
            {
                result.Append("FLIGHT MAY NOT PROCEED");
                var alternatives = _aircraftService.FindAlternativeAircraft(flightService.SeatsTaken).ToList();
                if (alternatives.Any())
                {
                    result.Append(_newLine);
                    result.AppendLine("Other more suitable aircraft are:");
                    foreach (var plane in alternatives)
                    {
                        result.AppendLine($"{plane.Name} could handle this flight");
                    }
                }
            }

            return result.ToString();
        }
    }
}

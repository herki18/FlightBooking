using System;
using FlightBooking.Core;
using FlightBooking.Core.Formats;
using FlightBooking.Core.Models.Passengers;
using FlightBooking.Core.Rules;
using FlightBooking.Core.Services;

namespace FlightBooking.Console
{
    internal class Program
    {
        private static ScheduledFlight _scheduledFlight ;

        private static void Main(string[] args)
        {
            SetupAirlineData();
            
            string command;
            do
            {
                System.Console.WriteLine("Please enter command.");
                command = System.Console.ReadLine() ?? "";
                var enteredText = command.ToLower();
                if (enteredText.Contains("print summary"))
                {
                    _scheduledFlight.Process();
                    System.Console.WriteLine();
                    System.Console.WriteLine(_scheduledFlight.GetSummary());
                }
                else if (enteredText.Contains("add general"))
                {
                    var passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new GeneralPassenger(passengerSegments[2], Convert.ToInt32(passengerSegments[3])));
                }
                else if (enteredText.Contains("add loyalty"))
                {
                    var passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(
                        new LoyaltyPassenger(passengerSegments[2],
                            Convert.ToInt32(passengerSegments[3]), 
                            Convert.ToInt32(passengerSegments[4]), 
                            Convert.ToBoolean(passengerSegments[5])));
                }
                else if (enteredText.Contains("add airline"))
                {
                    var passengerSegments = enteredText.Split(' ');
                    _scheduledFlight.AddPassenger(new AirlineEmployeePassenger(passengerSegments[2], Convert.ToInt32(passengerSegments[3])));
                }
                else if (enteredText.Contains("exit"))
                {
                    Environment.Exit(1);
                }
                else
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("UNKNOWN INPUT");
                    System.Console.ResetColor();
                }
            } while (command != "exit");
        }

        private static void SetupAirlineData()
        {
            var londonToParis = new FlightRoute("London", "Paris")
            {
                BaseCost = 50, 
                BasePrice = 100, 
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.7
            };

            var passengersService = new PassengerService();
            var aircraftService = new AircraftService();
            aircraftService.AddPlanes(new Plane { Id = 123, Name = "Antonov AN-2", NumberOfSeats = 12 });
            var flightService = new FlightService(londonToParis);
            var calculationService = new CalculationService();
            calculationService.AddRules(new ProfitRule(), new RelaxedRule());
            _scheduledFlight = new ScheduledFlight(
                flightService, 
                passengersService, 
                aircraftService, 
                calculationService, 
                new SummaryFormat(passengersService, aircraftService));

            _scheduledFlight.SetCurrentRule<ProfitRule>();
            _scheduledFlight.SetAircraftForRoute(123);
        }
    }
}

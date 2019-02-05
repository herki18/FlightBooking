using FlightBooking.Core.Formats;
using FlightBooking.Core.Models.Passengers;
using FlightBooking.Core.Rules;
using FlightBooking.Core.Services;
using Xunit;
using Xunit.Abstractions;

namespace FlightBooking.Core.Integration.Tests
{
    public class ScheduledFlightTests
    {
        private readonly ITestOutputHelper _output;

        public ScheduledFlightTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Ensure_That_Original_Use_Case_Works()
        {
            var passengerService = new PassengerService();
            var aircraftService = new AircraftService();
            aircraftService.AddPlanes(TestData.AntonovPlane);
            var calculationService = new CalculationService();
            calculationService.AddRules(new ProfitRule(), new RelaxedRule());
            var flightService = new FlightService(TestData.LondonToParis);
            var scheduledFlight = new ScheduledFlight(
                flightService, 
                passengerService, 
                aircraftService,
                calculationService, 
                new SummaryFormat(passengerService, aircraftService));

            scheduledFlight.SetAircraftForRoute(123);
            scheduledFlight.SetCurrentRule<ProfitRule>();

            scheduledFlight.AddPassenger(TestData.SteveGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.MarkGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.JamesGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.JaneGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.AlanGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.SuzyGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.JohnLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.SarahLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.JackLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.TrevorAirlineEmployeePassenger);

            scheduledFlight.Process();


            Assert.Equal(10, passengerService.GetCountForPassengerType<Passenger>());
            Assert.Equal(6, passengerService.GetCountForPassengerType<GeneralPassenger>());
            Assert.Equal(3, passengerService.GetCountForPassengerType<LoyaltyPassenger>());
            Assert.Equal(1, passengerService.GetCountForPassengerType<AirlineEmployeePassenger>());

            Assert.Equal(13, flightService.TotalExpectedBaggage);
            Assert.Equal(800, flightService.ProfitFromFlight);
            Assert.Equal(500, flightService.CostOfFlight);
            Assert.Equal(10, flightService.TotalLoyaltyPointsAccrued);
            Assert.Equal(100, flightService.TotalLoyaltyPointsRedeemed);
            Assert.Equal(300, flightService.ProfitSurplus);
            Assert.Equal(
                @"Flight summary for London to Paris

Total passengers: 10
    General sales: 6
    Loyalty member sales: 3
    Airline employee comps: 1

Total expected baggage: 13

Total revenue from flight: 800
Total costs from flight: 500
Flight generating profit of: 300

Total loyalty points given away: 10
Total loyalty points redeemed: 100


THIS FLIGHT MAY PROCEED",
                scheduledFlight.GetSummary());
        }

        [Fact]
        public void Should_Choose_Bigger_Airplane()
        {
            var passengerService = new PassengerService();
            var aircraftService = new AircraftService();
            aircraftService
                .AddPlanes(TestData.SmallAntonovPlane,
                    TestData.BombardierPlane,
                    TestData.ATRPlane);
            var calculationService = new CalculationService();
            calculationService.AddRules(new ProfitRule(), new RelaxedRule());
            var flightService = new FlightService(TestData.LondonToParis);
            var scheduledFlight = new ScheduledFlight(
                flightService,
                passengerService, 
                aircraftService, 
                calculationService, 
                new SummaryFormat(passengerService, aircraftService));
            scheduledFlight.SetAircraftForRoute(122);
            scheduledFlight.SetCurrentRule<ProfitRule>();

            scheduledFlight.AddPassenger(TestData.SteveGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.MarkGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.JamesGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.JaneGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.AlanGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.SuzyGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.AnthonyGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.MikeGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.JohnLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.SarahLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.JackLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.TrevorAirlineEmployeePassenger);

            scheduledFlight.Process();
            _output.WriteLine(scheduledFlight.GetSummary());

            Assert.Equal(12, passengerService.GetCountForPassengerType<Passenger>());
            Assert.Equal(8, passengerService.GetCountForPassengerType<GeneralPassenger>());
            Assert.Equal(3, passengerService.GetCountForPassengerType<LoyaltyPassenger>());
            Assert.Equal(1, passengerService.GetCountForPassengerType<AirlineEmployeePassenger>());

            Assert.Equal(15, flightService.TotalExpectedBaggage);
            Assert.Equal(1000, flightService.ProfitFromFlight);
            Assert.Equal(600, flightService.CostOfFlight);
            Assert.Equal(10, flightService.TotalLoyaltyPointsAccrued);
            Assert.Equal(100, flightService.TotalLoyaltyPointsRedeemed);
            Assert.Equal(400, flightService.ProfitSurplus);
            Assert.Equal(
                @"Flight summary for London to Paris

Total passengers: 12
    General sales: 8
    Loyalty member sales: 3
    Airline employee comps: 1

Total expected baggage: 15

Total revenue from flight: 1000
Total costs from flight: 600
Flight generating profit of: 400

Total loyalty points given away: 10
Total loyalty points redeemed: 100


FLIGHT MAY NOT PROCEED
Other more suitable aircraft are:
Bombardier Q400 could handle this flight
ATR 640 could handle this flight
",
                scheduledFlight.GetSummary());
        }

        [Fact]
        public void Should_Add_Discounted_Passenger()
        {
            var passengerService = new PassengerService();
            var aircraftService = new AircraftService();
            aircraftService.AddPlanes(TestData.AntonovPlane);
            var calculationService = new CalculationService();
            calculationService.AddRules(new ProfitRule(), new RelaxedRule());
            var flightService = new FlightService(TestData.LondonToParis);
            var scheduledFlight = new ScheduledFlight(
                flightService,
                passengerService, 
                aircraftService, 
                calculationService, 
                new SummaryFormat(passengerService, aircraftService));

            scheduledFlight.SetAircraftForRoute(123);
            scheduledFlight.SetCurrentRule<ProfitRule>();

            scheduledFlight.AddPassenger(TestData.SteveGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.MarkGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.JaneGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.AlanGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.SuzyGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.AnthonyGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.MikeGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.JohnLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.SarahLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.JackLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.TrevorAirlineEmployeePassenger);
            scheduledFlight.AddPassenger(TestData.MikeDiscountedPassenger);

            scheduledFlight.Process();

            Assert.Equal(12, passengerService.GetCountForPassengerType<Passenger>());
            Assert.Equal(7, passengerService.GetCountForPassengerType<GeneralPassenger>());
            Assert.Equal(3, passengerService.GetCountForPassengerType<LoyaltyPassenger>());
            Assert.Equal(1, passengerService.GetCountForPassengerType<DiscountedPassenger>());
            Assert.Equal(1, passengerService.GetCountForPassengerType<AirlineEmployeePassenger>());

            Assert.Equal(14, flightService.TotalExpectedBaggage);
            Assert.Equal(950, flightService.ProfitFromFlight);
            Assert.Equal(600, flightService.CostOfFlight);
            Assert.Equal(10, flightService.TotalLoyaltyPointsAccrued);
            Assert.Equal(100, flightService.TotalLoyaltyPointsRedeemed);
            Assert.Equal(350, flightService.ProfitSurplus);
        }

        [Fact]
        public void Should_Return_Same_Data_When_Calling_Process_Twice()
        {
            var passengerService = new PassengerService();
            var aircraftService = new AircraftService();
            aircraftService.AddPlanes(TestData.AntonovPlane);
            var calculationService = new CalculationService();
            calculationService.AddRules(new ProfitRule(), new RelaxedRule());
            var flightService = new FlightService(TestData.LondonToParis);
            var scheduledFlight = new ScheduledFlight(
                flightService,
                passengerService, 
                aircraftService, 
                calculationService, 
                new SummaryFormat(passengerService, aircraftService));

            scheduledFlight.SetAircraftForRoute(123);
            scheduledFlight.SetCurrentRule<ProfitRule>();

            scheduledFlight.AddPassenger(TestData.SteveGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.MarkGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.JamesGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.JaneGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.AlanGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.SuzyGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.AnthonyGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.MikeGeneralPassenger);
            scheduledFlight.AddPassenger(TestData.JohnLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.SarahLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.JackLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.TrevorAirlineEmployeePassenger);
            scheduledFlight.AddPassenger(TestData.MikeDiscountedPassenger);

            scheduledFlight.Process();
            scheduledFlight.Process();

            Assert.Equal(13, passengerService.GetCountForPassengerType<Passenger>());
            Assert.Equal(8, passengerService.GetCountForPassengerType<GeneralPassenger>());
            Assert.Equal(3, passengerService.GetCountForPassengerType<LoyaltyPassenger>());
            Assert.Equal(1, passengerService.GetCountForPassengerType<DiscountedPassenger>());
            Assert.Equal(1, passengerService.GetCountForPassengerType<AirlineEmployeePassenger>());

            Assert.Equal(15, flightService.TotalExpectedBaggage);
            Assert.Equal(1050, flightService.ProfitFromFlight);
            Assert.Equal(650, flightService.CostOfFlight);
            Assert.Equal(10, flightService.TotalLoyaltyPointsAccrued);
            Assert.Equal(100, flightService.TotalLoyaltyPointsRedeemed);
            Assert.Equal(400, flightService.ProfitSurplus);
        }

        [Fact]
        public void When_Relaxed_Rule_Is_Used_Should_Proceed_With_Flight()
        {
            var passengerService = new PassengerService();
            var aircraftService = new AircraftService();
            aircraftService.AddPlanes(TestData.SmallAntonovPlane);
            var calculationService = new CalculationService();
            calculationService.AddRules(new ProfitRule(), new RelaxedRule());
            var flightService = new FlightService(TestData.LondonToParis);
            var scheduledFlight = new ScheduledFlight(
                flightService,
                passengerService,
                aircraftService,
                calculationService,
                new SummaryFormat(passengerService, aircraftService));

            scheduledFlight.SetAircraftForRoute(122);

            scheduledFlight.SetCurrentRule<RelaxedRule>();

            scheduledFlight.AddPassenger(TestData.JackLoyaltyPassenger);
            scheduledFlight.AddPassenger(TestData.TrevorAirlineEmployeePassenger);
            scheduledFlight.AddPassenger(TestData.SunnyAirlineEmployeePassenger);
            scheduledFlight.AddPassenger(TestData.SunnyAirlineEmployeePassenger);
            scheduledFlight.AddPassenger(TestData.MartinAirlineEmployeePassenger);
            scheduledFlight.AddPassenger(TestData.MartinAirlineEmployeePassenger);
            scheduledFlight.AddPassenger(TestData.MikeDiscountedPassenger);

            scheduledFlight.Process();

            Assert.True(flightService.CanFlightProceed);
            Assert.Equal(-200, flightService.ProfitSurplus);
        }
    }
}

using FlightBooking.Core.Interfaces;

namespace FlightBooking.Core.Services
{
    public class FlightService : IFlightService
    {
        public int TotalExpectedBaggage { get; set; }
        public double ProfitFromFlight { get; set; }
        public double CostOfFlight { get; set; }
        public int TotalLoyaltyPointsAccrued { get; set; }
        public int TotalLoyaltyPointsRedeemed { get; set; }
        public double ProfitSurplus { get; set; }
        public int SeatsTaken { get; set; }
        public bool CanFlightProceed { get; set; }
        public FlightRoute FlightRoute { get; set; }
        public Plane Aircraft { get; set; }

        public FlightService(FlightRoute route)
        {
            FlightRoute = route;
        }

        public void AddAircraft(Plane plane)
        {
            Aircraft = plane;
        }

        public void Reset()
        {
            TotalExpectedBaggage = 0;
            ProfitFromFlight = 0;
            CostOfFlight = 0;
            TotalLoyaltyPointsAccrued = 0;
            TotalLoyaltyPointsRedeemed = 0;
            ProfitSurplus = 0;
            SeatsTaken = 0;
            CanFlightProceed = false;
        }
    }
}

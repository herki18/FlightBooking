namespace FlightBooking.Core.Interfaces
{
    public interface IFlightService
    {
        int TotalExpectedBaggage { get; set; }
        double ProfitFromFlight { get; set; }
        double CostOfFlight { get; set; }
        int TotalLoyaltyPointsAccrued { get; set; }
        int TotalLoyaltyPointsRedeemed { get; set; }
        double ProfitSurplus { get; set; }
        int SeatsTaken { get; set; }
        bool CanFlightProceed { get; set; }
        FlightRoute FlightRoute { get; set; }
        Plane Aircraft { get; set; }
        
        void AddAircraft(Plane plane);
        void Reset();
    }
}

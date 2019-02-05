using System;
using FlightBooking.Core.Interfaces;

namespace FlightBooking.Core.Models.Passengers
{
    public class LoyaltyPassenger : Passenger
    {
        public int LoyaltyPoints { get; set; }
        public bool IsUsingLoyaltyPoints { get; set; }

        public LoyaltyPassenger(string name, int age, int loyaltyPoints, bool isUsingLoyaltyPoints) : base(name, age)
        {
            LoyaltyPoints = loyaltyPoints;
            IsUsingLoyaltyPoints = isUsingLoyaltyPoints;
            AllowedBags = 2;
        }

        public override void Calculate(IFlightService flightService)
        {
            base.Calculate(flightService);

            if (IsUsingLoyaltyPoints)
            {
                var loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(flightService.FlightRoute.BasePrice));
                LoyaltyPoints -= loyaltyPointsRedeemed;
                flightService.TotalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
            }
            else
            {
                flightService.TotalLoyaltyPointsAccrued += flightService.FlightRoute.LoyaltyPointsGained;
                flightService.ProfitFromFlight += flightService.FlightRoute.BasePrice;
            }
        }
    }
}

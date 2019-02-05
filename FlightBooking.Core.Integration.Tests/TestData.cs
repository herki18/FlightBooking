using FlightBooking.Core.Models.Passengers;

namespace FlightBooking.Core.Integration.Tests
{
    public static class TestData
    {
        public static FlightRoute LondonToParis
        {
            get
            {
                var londonToParis = new FlightRoute("London", "Paris")
                {
                    BaseCost = 50,
                    BasePrice = 100,
                    LoyaltyPointsGained = 5,
                    MinimumTakeOffPercentage = 0.7
                };

                return londonToParis;
            }
        }

        public static Plane SmallAntonovPlane => new Plane {Id = 122, Name = "Antonov AN-1", NumberOfSeats = 7};
        public static Plane AntonovPlane => new Plane {Id = 123, Name = "Antonov AN-2", NumberOfSeats = 12};
        public static Plane BombardierPlane => new Plane { Id = 124, Name = "Bombardier Q400", NumberOfSeats = 15 };
        public static Plane ATRPlane => new Plane { Id = 125, Name = "ATR 640", NumberOfSeats = 13 };

        public static GeneralPassenger SteveGeneralPassenger => new GeneralPassenger("Steve", 30);
        public static GeneralPassenger MarkGeneralPassenger => new GeneralPassenger("Mark", 12);
        public static GeneralPassenger JamesGeneralPassenger => new GeneralPassenger("James", 36);
        public static GeneralPassenger JaneGeneralPassenger => new GeneralPassenger("Jane", 32);
        public static GeneralPassenger AlanGeneralPassenger => new GeneralPassenger("Alan", 34);
        public static GeneralPassenger SuzyGeneralPassenger => new GeneralPassenger("Suzy", 21);
        public static GeneralPassenger AnthonyGeneralPassenger => new GeneralPassenger("Anthony", 21);
        public static GeneralPassenger MikeGeneralPassenger => new GeneralPassenger("Mike", 21);

        public static LoyaltyPassenger JohnLoyaltyPassenger => new LoyaltyPassenger("John", 29, 1000, true);
        public static LoyaltyPassenger SarahLoyaltyPassenger => new LoyaltyPassenger("Sarah", 45, 1250, false);
        public static LoyaltyPassenger JackLoyaltyPassenger => new LoyaltyPassenger("Jack", 60, 50, false);

        public static AirlineEmployeePassenger TrevorAirlineEmployeePassenger => new AirlineEmployeePassenger("Trevor", 47);
        public static AirlineEmployeePassenger SunnyAirlineEmployeePassenger => new AirlineEmployeePassenger("Sunny", 20);
        public static AirlineEmployeePassenger MartinAirlineEmployeePassenger => new AirlineEmployeePassenger("Martin", 25);

        public static DiscountedPassenger MikeDiscountedPassenger => new DiscountedPassenger("Mike", 21);
    }
}

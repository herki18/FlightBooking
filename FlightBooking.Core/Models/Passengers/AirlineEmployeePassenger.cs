namespace FlightBooking.Core.Models.Passengers
{
    public class AirlineEmployeePassenger : Passenger
    {
        public AirlineEmployeePassenger(string name, int age) : base(name, age)
        {
            AllowedBags = 1;
        }
    }
}

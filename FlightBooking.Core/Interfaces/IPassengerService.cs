using System;
using System.Collections.Generic;
using FlightBooking.Core.Models.Passengers;

namespace FlightBooking.Core.Interfaces
{
    public interface IPassengerService
    {
        void AddPassenger(Passenger passenger);
        int GetCountForPassengerType<T>() where T : Passenger;
        int GetCountForPassengerType(Type type);
        IEnumerable<T> GetPassengersByType<T>() where T : Passenger;
        IEnumerable<Type> GetPassengersTypes();
        void Calculate(IFlightService flightService);
    }
}

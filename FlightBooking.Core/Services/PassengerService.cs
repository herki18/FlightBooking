using System;
using System.Collections.Generic;
using System.Linq;
using FlightBooking.Core.Interfaces;
using FlightBooking.Core.Models.Passengers;

namespace FlightBooking.Core.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly List<Passenger> _passengers;
        private readonly List<Type> _passengerTypes;

        public PassengerService()
        {
            _passengerTypes = new List<Type>();
            _passengers = new List<Passenger>();
        }

        public void AddPassenger(Passenger passenger)
        {
            _passengers.Add(passenger);
            if (!_passengerTypes.Contains(passenger.GetType()))
            {
                _passengerTypes.Add(passenger.GetType());
            }
        }

        public int GetCountForPassengerType<T>() where T : Passenger
        {
            return _passengers.OfType<T>().Count();
        }

        public int GetCountForPassengerType(Type type)
        {
            return _passengers.Count(x => x.GetType() == type);
        }

        public IEnumerable<T> GetPassengersByType<T>() where T : Passenger
        {
            return _passengers.OfType<T>();
        }

        public IEnumerable<Type> GetPassengersTypes()
        {
            return _passengerTypes;
        }

        public void Calculate(IFlightService flightService)
        {
            foreach (var passenger in _passengers)
            {
                passenger.Calculate(flightService);
            }
        }
    }
}

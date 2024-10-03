using Garage.Models;
using Garage.Models.Vehicles;
using Garage.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Handlers
{
    internal class GarageHandler<T> where T : Vehicle
    {
        private Garage<T> _garage;

        public GarageHandler()
        {
            CreateNewGarage();
        }

        private void CreateNewGarage(bool createWithCapacityMessage = false)
        {
            if (createWithCapacityMessage)
            {
                _garage = new Garage<T>(ConsoleUI<T>.GetUserInputForInt("Enter the capacity for the new Garage: "));
            }
            _garage = new Garage<T>(8) { };
        }

        public void AddVehicleToGarage(T vehicle)
        {
            _garage.Add(vehicle);
        }
        public void RemoveVehicleToGarage(T vehicle)
        {
            _garage.Remove(vehicle);
        }
        public List<string> GetVehiclesInfo()
        {
            List<string> vehicleInfo = new List<string>();
            foreach (var vehicle in _garage)
            {
                if (vehicle != null)
                {
                    vehicleInfo.Add(vehicle.ToString());
                    if (vehicleInfo.Count == _garage.Count)
                    {
                        return vehicleInfo;
                    }
                }
            }
            return vehicleInfo;
        }
        public List<string> GetVehiclesInfo(int registrationNumber, Colour colour, int wheels)
        {
            List<string> vehicleInfo = new List<string>();
            foreach (var vehicle in _garage)
            {
                if (vehicle != null)
                {
                    vehicleInfo.Add(vehicle.ToString());
                    if (vehicleInfo.Count == _garage.Count)
                    {
                        return vehicleInfo;
                    }
                }
            }
            return vehicleInfo;
        }

        internal string GetVehicleTypesWithCount()
        {
            int numberOfBoats = 0;
            int numberOfCars = 0;
            int numberOfMotorCycles = 0;
            foreach (var vehicle in _garage)
            {
                if (vehicle is Boat)
                {
                    numberOfBoats++;   
                }
                if (vehicle is Car)
                {
                    numberOfCars++;   
                }
                if (vehicle is Motorcycle)
                {
                    numberOfMotorCycles++;   
                }
            }

            return $"Boats: {numberOfBoats} \nCars: {numberOfCars} \nMotorcycles: {numberOfMotorCycles}";
        }
    }
}

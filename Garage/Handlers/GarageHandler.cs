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
    public class GarageHandler
    {
        private Garage<IVehicle> _garage;
        public GarageHandler()
        {
            CreateNewGarage();
        }

        private void CreateNewGarage(bool createWithCapacityMessage = false)
        {
            if (createWithCapacityMessage)
            {
                _garage = new Garage<IVehicle>(ConsoleUI.GetIntInputFromUser("Enter the capacity for the new Garage: ", false));
            }
            _garage = new Garage<IVehicle>(8) { };
        }

        public bool AddVehicleToGarage(IVehicle vehicle)
        {
            return _garage.Add(vehicle);
        }
        public bool AddVehicleToGarage(int registrationNumber, Colour colour, int wheels, double? length = null, FuelType? fuelType = null, double? cylinderVolume = null)
        {
            return _garage.Add(CreateVehicle(registrationNumber, colour, wheels, length,  fuelType, cylinderVolume));
        }

        public IVehicle CreateVehicle(int registrationNumber, Colour colour, int wheels, double? length = null, FuelType? fuelType = null, double? cylinderVolume = null)
        {
            if (length != null)
            {
                return new Boat() { RegistrationNumber = registrationNumber, Colour = colour, Wheels = wheels, Length = (double)length};
            }
            if (fuelType != null)
            {
                return new Car() { RegistrationNumber = registrationNumber, Colour = colour, Wheels = wheels, FuelType = (FuelType)fuelType};
            }
            if (cylinderVolume != null)
            {
                return new Motorcycle() { RegistrationNumber = registrationNumber, Colour = colour, Wheels = wheels, CylinderVolume = (double)cylinderVolume};
            }
            throw new Exception("Vehicle not implemented");
        }
        public bool RemoveVehicleFromGarage(int regNr)
        {
            return _garage.Remove(regNr);
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

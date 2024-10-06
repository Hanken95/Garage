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
    public class GarageHandler: IHandler
    {
        private Garage<Vehicle> _garage;
        public GarageHandler()
        {
            CreateNewGarage();
        }

        public void CreateNewGarage(bool createWithCapacityMessage = false)
        {
            if (createWithCapacityMessage)
            {
                _garage = new Garage<Vehicle>(ConsoleUI.GetIntInputFromUser("Enter the capacity for the new Garage: ", false));
                return;
            }
            _garage = new Garage<Vehicle>(8) { };
        }

        public bool CheckIfGarageIsFull()
        {
            return _garage.IsFull;
        }

        public bool AddVehicleToGarage(Vehicle vehicle)
        {
            return _garage.Add(vehicle);
        }

        public bool AddVehicleToGarage(int registrationNumber, Colour colour, int wheels, double? length = null, FuelType? fuelType = null, double? cylinderVolume = null)
        {
            return _garage.Add(CreateVehicle(registrationNumber, colour, wheels, length,  fuelType, cylinderVolume));
        }

        public Vehicle CreateVehicle(int registrationNumber, Colour? colour, int wheels, double? length = null, FuelType? fuelType = null, double? cylinderVolume = null)
        {
            if (length != null)
            {
                return new Boat() { RegistrationNumber = registrationNumber, Colour = (Colour)colour, Wheels = 0, Length = (double)length};
            }
            if (fuelType != null)
            {
                return new Car() { RegistrationNumber = registrationNumber, Colour = (Colour)colour, Wheels = wheels, FuelType = (FuelType)fuelType};
            }
            if (cylinderVolume != null)
            {
                return new Motorcycle() { RegistrationNumber = registrationNumber, Colour = (Colour)colour, Wheels = wheels, CylinderVolume = (double)cylinderVolume};
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
            for (int i = 0; i < _garage.Capacity; i++)
            {
                if (_garage[i] != null)
                {
                    vehicleInfo.Add(_garage[i].ToString());
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

        public string GetVehicleTypesWithCount()
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

        public List<Vehicle> GetVehicles()
        {
            return _garage.ToList();
        }

        public List<Vehicle> GetVehicles(int regNr = -1, Colour? colour = null, int wheels = -1)
        {
            var vehicleList = _garage.ToList();
            if (regNr != -1)
            {
                vehicleList = vehicleList.Where(vehicle => vehicle.RegistrationNumber == regNr).ToList();
            }
            if (colour != null)
            {
                vehicleList = vehicleList.Where(vehicle => vehicle.Colour == colour).ToList();
            }
            if(wheels != -1)
            {
                vehicleList = vehicleList.Where(vehicle => vehicle.Colour == colour).ToList();
            }
            return vehicleList;
            
        }
        public List<Boat> GetBoats(int regNr = -1, Colour? colour = null, int wheels = -1, double length = -1)
        {
            List<Vehicle> vehicles = GetVehicles(regNr, colour, wheels);
            if (length != -1)
            {
                return vehicles.OfType<Boat>().Where(boat => boat.Length == length).ToList();
            }
            return vehicles.OfType<Boat>().ToList();
        }
        public List<Car> GetCars(int regNr = -1, Colour? colour = null, int wheels = -1, FuelType? fuelType = null)
        {
            List<Vehicle> vehicles = GetVehicles(regNr, colour, wheels);
            if (fuelType != null)
            {
                return vehicles.OfType<Car>().Where(boat => boat.FuelType == fuelType).ToList();
            }
            return vehicles.OfType<Car>().ToList();
        }
        public List<Motorcycle> GetMotorcycles(int regNr = -1, Colour? colour = null, int wheels = -1, double cylinderVolume = -1)
        {
            List<Vehicle> vehicles = GetVehicles(regNr, colour, wheels);
            if (cylinderVolume != -1)
            {
                return vehicles.OfType<Motorcycle>().Where(boat => boat.CylinderVolume == cylinderVolume).ToList();
            }
            return vehicles.OfType<Motorcycle>().ToList();
        }

        public bool PopulateGarage()
        {
            _garage.Clear();
            AddVehicleToGarage(new Car() { RegistrationNumber = 15123, Colour = Colour.Blue, Wheels = 4, FuelType = FuelType.Petrol});
            AddVehicleToGarage(new Boat() { RegistrationNumber = 15413, Colour = Colour.Blue, Wheels = 0, Length = 2.5});
            return AddVehicleToGarage(new Car() { RegistrationNumber = 1523123, Colour = Colour.Purple, Wheels = 4, FuelType = FuelType.Electric});
        }
    }
}

using Garage.Handlers;
using Garage.Models;
using Garage.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.UI
{
    internal class ConsoleUI : IUI
    {
        private static GarageHandler _garageHandler;

        public static void MainMenu(GarageHandler garageHandler)
        {
            _garageHandler = garageHandler;
            while (true)
            {
                Console.WriteLine("This is the main menu." +
                    "\nPress the number to go to the corresponding menu." +
                    "\n1: View all vehicles in the garage." +
                    "\n2: View all vehicles types and how many there are of each type." +
                    "\n3: Add new vehicle." +
                    "\n4: Remove vehicle." +
                    "\n5: View all vehicles that meet specified criteria." +
                    "\n6: Create a new garage(deletes the old one)." +
                    "\n7: Populate with stock vehicles(Removes current vehicles)" +
                    "\n0: Quit.");

                char input = Console.ReadKey(true).KeyChar;
                Console.Clear();
                switch (input)
                {
                    case '1':
                        ViewVehicles(_garageHandler.GetVehiclesInfo());
                        break;
                    case '2':
                        ViewVehicleTypesWithCount();
                        break;
                    case '3':
                        AddVehicleToGarage();
                        break;
                    case '4':
                        RemoveVehicleFromGarage();
                        break;
                    case '5':
                        DisplayVehiclesBasedOnProperties();
                        break;
                    case '6':
                        CreateNewGarage();
                        break;
                    case '7':
                        PupulateGarage();
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("Wrong input, try again");
                        break;
                }
                WaitForUserInputToContinue();
            }
        }

        

        public static int GetIntInputFromUser(string message, bool canBeBelowOne)
        {
            Console.Write(message);
            try
            {
                if (canBeBelowOne)
                {
                    return int.Parse(Console.ReadLine());
                }
                else
                {
                    var value = int.Parse(Console.ReadLine());
                    if (value < 1)
                    {
                        Console.WriteLine("Cannot be below 1");
                        WaitForUserInputToContinue();
                        return GetIntInputFromUser(message, canBeBelowOne);
                    }
                    return value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong input. Error: " + e.Message);
                WaitForUserInputToContinue();
                return GetIntInputFromUser(message, canBeBelowOne);
            }
        }

        public static double GetDoubleInputFromUser(string message, bool canBeZeroOrLower)
        {
            Console.Write(message);
            try
            {
                if (canBeZeroOrLower)
                {
                    return double.Parse(Console.ReadLine());
                }
                else
                {
                    var value = double.Parse(Console.ReadLine());
                    if (value <= 0)
                    {
                        Console.WriteLine("Cannot be 0 or lower");
                        WaitForUserInputToContinue();
                        return GetDoubleInputFromUser(message, canBeZeroOrLower);
                    }
                    return value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong input. Error: " + e.Message);
                WaitForUserInputToContinue();
                return GetDoubleInputFromUser(message, canBeZeroOrLower);
            }
        }

        private static string GetStringInputFromUser(string message, bool vehicleTypeSelection = false)
        {
            Console.WriteLine(message);
            if (!vehicleTypeSelection)
            {
                return Console.ReadLine();
            }
            var input = Console.ReadLine().ToLower();
            if (input == "boat" || input == "car" || input == "motorcycle")
            {
                return input;
            }
            Console.WriteLine("Wrong input, can only be the available vehicles.");
            WaitForUserInputToContinue();
            return GetStringInputFromUser(message, vehicleTypeSelection);
        }

        private static Colour? GetColourInputFromUser(string message, bool defaultToRed)
        {
            var newMessage = message + "\nAvailable colours are: ";
            foreach (var colour in Enum.GetNames(typeof(Colour)))
            {
                newMessage += colour + " ";
            }
            var chosenColour = GetStringInputFromUser(newMessage);
            if (string.IsNullOrEmpty(chosenColour))
            {
                if (defaultToRed)
                {
                    return Colour.Red;
                }
                else
                {
                    return null;
                }
            }
            try
            {
                return (Colour)Enum.Parse(typeof(Colour), chosenColour);
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong input. Error: " + e.Message);
                WaitForUserInputToContinue();
                return GetColourInputFromUser(message, defaultToRed);
            }
        }

        private static FuelType? GetFuelTypeInputFromUser(string message, bool defaultToPetrol)
        {
            var newMessage = message + "\nAvailable fuel types are: ";
            foreach (var colour in Enum.GetNames(typeof(FuelType)))
            {
                newMessage += colour + " ";
            }
            var chosenColour = GetStringInputFromUser(newMessage);
            if (string.IsNullOrEmpty(chosenColour))
            {
                if (defaultToPetrol)
                {
                    return FuelType.Petrol;
                }
                else
                {
                    return null;
                }

            }
            try
            {
                return (FuelType)Enum.Parse(typeof(FuelType), chosenColour);
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong input. Error: " + e.Message);
                WaitForUserInputToContinue();
                return GetFuelTypeInputFromUser(message, defaultToPetrol);
            }
        }

        private static void WaitForUserInputToContinue(bool clearConsole = true)
        {
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey(true);
            if (clearConsole)
            {
                Console.Clear();
            }
        }

        private static void ViewVehicles(List<string> vehicleInfos)
        {
            foreach (var vehicleInfo in vehicleInfos)
            {
                Console.WriteLine($"Vehicle number {vehicleInfos.IndexOf(vehicleInfo) + 1}: {vehicleInfo}");
            }
        }

        private static void ViewVehicleTypesWithCount()
        {
            Console.WriteLine(_garageHandler.GetVehicleTypesWithCount());
        }
        
        private static void AddVehicleToGarage()
        {
            if (!_garageHandler.CheckIfGarageIsFull())
            {
                var vehicleType = GetStringInputFromUser("Choose vehicle type. Vehicles available: Car,Boat, Motorcycle", true);
                Console.Clear();
                var regNr = GetIntInputFromUser("Enter the registration number for the vehicle: ", false);
                Console.Clear();
                Colour colour = (Colour)GetColourInputFromUser("Enter the colour for the vehicle, if you enter nothing then red will be chosen", true);
                Console.Clear();
                int wheels = 0;
                if (vehicleType != "boat")
                {
                    wheels = GetIntInputFromUser("Enter how many wheels the vehicle has: ", false);
                }
                Console.Clear();
                bool vehicleAdded = false;
                if (vehicleType == "boat")
                {
                    vehicleAdded = _garageHandler.AddVehicleToGarage(regNr, colour, wheels, length: GetDoubleInputFromUser("Enter the length for the boat: ", false));
                }
                else if (vehicleType == "car")
                {
                    vehicleAdded = _garageHandler.AddVehicleToGarage(regNr, colour, wheels, fuelType: (FuelType)GetFuelTypeInputFromUser("Enter the fuel type for the car. If you enter nothing then petrol will be chosen", true));
                }
                else if (vehicleType == "motorcycle")
                {
                    vehicleAdded = _garageHandler.AddVehicleToGarage(regNr, colour, wheels, cylinderVolume: GetDoubleInputFromUser("Enter the cylinder volume for the motorcycle: ", false));
                }
                else
                {
                    throw new Exception("You managed to break the garage by entering a incorrect vehicle type.");
                }
                Console.Clear();
                if (vehicleAdded)
                {
                    Console.WriteLine("Vehicle successfully added.");
                }
                else
                {
                    Console.WriteLine("Vehicle was not succeffully added.");
                }
            }
            else
            {
                Console.WriteLine("Garage is full.");
            }
        }

        private static void RemoveVehicleFromGarage()
        {
            int regNr = GetIntInputFromUser("Enter the registration number of the vehicle to be removed", false);

            bool vehicleRemoved = _garageHandler.RemoveVehicleFromGarage(regNr);
            Console.Clear();
            if (vehicleRemoved)
            {
                Console.WriteLine("Vehicle removed");
            }
            else
            {
                Console.WriteLine("Vehicle not found");
            }
        }

        private static void DisplayVehiclesBasedOnProperties()
        {
            var vehicleType = GetStringInputFromUser("Choose vehicle type. Vehicles available: Car,Boat, Motorcycle" +
                "\nIf you enter somethin else then all vehicle types will be selected", false);
            Console.Clear();
            var regNr = GetIntInputFromUser("Enter the registration number for the vehicles(enter -1 to not include this in the search): ", true);
            Console.Clear();
            Colour? colour = GetColourInputFromUser("Enter the colour for the vehicles, if you enter nothing then colour will be not included int he search", false);
            Console.Clear();
            var wheels = GetIntInputFromUser("Enter how many wheels the vehicles have(enter -1 to not include this in the search): ", true);
            Console.Clear();
            if (vehicleType == "boat")
            {
                var boats = _garageHandler.GetBoats(regNr, colour, wheels, GetDoubleInputFromUser("Enter length of the boats(enter -1 to not include this in the search): ", true));
                Console.Clear();
                foreach (var boat in boats)
                {
                    Console.WriteLine(boat);
                }
            }
            else if (vehicleType == "car")
            {
                var cars = _garageHandler.GetCars(regNr, colour, wheels, GetFuelTypeInputFromUser("Enter the fuel type for the cars. If you enter nothing then fuel type will not be included in the search", false));
                Console.Clear();
                foreach (var car in cars)
                {
                    Console.WriteLine(car);
                }
            }
            else if (vehicleType == "motorcycle")
            {
                var motorcycles = _garageHandler.GetMotorcycles(regNr, colour, wheels, GetDoubleInputFromUser("Enter cylinder volume of the motorcycles(enter -1 to not include this in the search): ", true));
                Console.Clear();
                foreach (var motorcycle in motorcycles)
                {
                    Console.WriteLine(motorcycle);
                }
            }
            else
            {
                var vehicles = _garageHandler.GetVehicles(regNr, colour, wheels);
                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine(vehicle);
                }
            }
        }

        private static void CreateNewGarage()
        {
            _garageHandler.CreateNewGarage(true);
        }

        private static void PupulateGarage()
        {
            _garageHandler.PopulateGarage();
            Console.WriteLine("Garage populated");
        }
    }
}

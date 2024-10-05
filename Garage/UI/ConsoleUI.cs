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
                    "\n0: Quit.");

                char input = Console.ReadKey(true).KeyChar;
                Console.Clear();
                switch (input)
                {
                    case '1':
                        ViewVehicles();
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
                        FindVehiclesBasedOnProperties();
                        break;
                    case '6':
                        CreateNewGarage();
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

        public static int GetIntInputFromUser(string message, bool canBeNegative)
        {
            Console.Write(message);
            try
            {
                if (canBeNegative)
                {
                    return int.Parse(Console.ReadLine());
                }
                else
                {
                    var value = int.Parse(Console.ReadLine());
                    if (value < 0)
                    {
                        Console.WriteLine("Cannot be negative");
                        WaitForUserInputToContinue();
                        return GetIntInputFromUser(message, canBeNegative);
                    }
                    return value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong input. Error: " + e.Message);
                WaitForUserInputToContinue();
                return GetIntInputFromUser(message, canBeNegative);
            }
        }
        public static double GetDoubleInputFromUser(string message, bool canBeNegative)
        {
            Console.Write(message);
            try
            {
                if (canBeNegative)
                {
                    return double.Parse(Console.ReadLine());
                }
                else
                {
                    var value = double.Parse(Console.ReadLine());
                    if (value < 0)
                    {
                        Console.WriteLine("Cannot be negative");
                        WaitForUserInputToContinue();
                        return GetDoubleInputFromUser(message, canBeNegative);
                    }
                    return value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong input. Error: " + e.Message);
                WaitForUserInputToContinue();
                return GetDoubleInputFromUser(message, canBeNegative);
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

        private static Colour GetColourInputFromUser(string message)
        {
            var newMessage = message + "\nAvailable colours are: ";
            foreach (var colour in Enum.GetNames(typeof(Colour)))
            {
                newMessage += colour + " ";
            }
            var chosenColour = GetStringInputFromUser(newMessage);
            if (string.IsNullOrEmpty(chosenColour))
            {
                return Colour.Red;
            }
            try
            {
                return (Colour)Enum.Parse(typeof(Colour), chosenColour);
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong input. Error: " + e.Message);
                WaitForUserInputToContinue();
                return GetColourInputFromUser(message);
            }
        }
        private static FuelType GetFuelTypeInputFromUser(string message)
        {
            var newMessage = message + "\nAvailable fuel types are: ";
            foreach (var colour in Enum.GetNames(typeof(FuelType)))
            {
                newMessage += colour + " ";
            }
            var chosenColour = GetStringInputFromUser(newMessage);
            if (string.IsNullOrEmpty(chosenColour))
            {
                return FuelType.Petrol;
            }
            try
            {
                return (FuelType)Enum.Parse(typeof(FuelType), chosenColour);
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong input. Error: " + e.Message);
                WaitForUserInputToContinue();
                return GetFuelTypeInputFromUser(message);
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

        private static void ViewVehicles()
        {
            foreach (var vehicleInfo in _garageHandler.GetVehiclesInfo())
            {
                Console.WriteLine(vehicleInfo);
            }
        }

        private static void ViewVehicleTypesWithCount()
        {
            Console.WriteLine(_garageHandler.GetVehicleTypesWithCount());
        }
        
        private static void AddVehicleToGarage()
        {
            var vehicleType = GetStringInputFromUser("Choose vehicle type. Vehicles available: Car,Boat, Motorcycle", true);
            Console.Clear();
            var regNr = GetIntInputFromUser("Enter the registration number for the vehicle", false);
            Console.Clear();
            Colour colour = GetColourInputFromUser("Enter the colour for the vehicle, if you enter nothing then red will be chosen");
            Console.Clear();
            var wheels = GetIntInputFromUser("Enter how many wheels the vehicle has", false);
            bool vehicleAdded = false;
            if (vehicleType == "boat")
            {
                vehicleAdded = _garageHandler.AddVehicleToGarage(regNr, colour, wheels, length: GetDoubleInputFromUser("Enter the length for the boat", false));
            }
            else if (vehicleType == "car")
            {
                vehicleAdded = _garageHandler.AddVehicleToGarage(regNr, colour, wheels, fuelType: GetFuelTypeInputFromUser("Enter the fuel type for the car. If you enter nothing then petrol will be chosen"));
            }
            else if (vehicleType == "motorcycle")
            {
                vehicleAdded = _garageHandler.AddVehicleToGarage(regNr, colour, wheels, cylinderVolume: GetDoubleInputFromUser("Enter the cylinder volume for the motorcycle.", false));
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

        private static void RemoveVehicleFromGarage()
        {
            int regNr = GetIntInputFromUser("Enter the registration number of the vehicle to be removed", false);

            if (_garageHandler.RemoveVehicleFromGarage(regNr))
            {
                
            }
        }

        private static void FindVehiclesBasedOnProperties()
        {
            throw new NotImplementedException();
        }

        private static void CreateNewGarage()
        {
            throw new NotImplementedException();
        }
    }
}

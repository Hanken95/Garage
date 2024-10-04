using Garage.Handlers;
using Garage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.UI
{
    internal static class ConsoleUI<T> where T : Vehicle
    {
        private static GarageHandler<T> _garageHandler;


        public static void MainMenu(GarageHandler<T> garageHandler)
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

        private static string GetStringInputFromUser(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        private static Colour GetColourInputFromUser(string message)
        {
            var newMessage = message + "\nAvailable colours are: ";
            foreach (var colour in Enum.GetNames(typeof(Colour)))
            {
                message += colour + " ";
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
            var regNr = GetIntInputFromUser("Enter the registration number for the vehicle", false);
            Console.Clear();
            Colour colour = GetColourInputFromUser("Enter the colour for the vehicle");
            Console.Clear();
            var wheels = GetIntInputFromUser("Enter how many wheels the car has", false);
        }


        private static void RemoveVehicleFromGarage()
        {
            throw new NotImplementedException();
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

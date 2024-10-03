using Garage.Handlers;
using Garage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.UI
{
    internal class ConsoleUI<T> where T : Vehicle
    {
        private GarageHandler<T> _garageHandler;

        public ConsoleUI(GarageHandler<T> garageHandler)
        {
            _garageHandler = garageHandler;
        }


        public void MainMenu()
        {
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

        public static int GetUserInputForInt(string message)
        {
            Console.Write(message);
            try
            {
                return int.Parse(Console.ReadLine());

            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong input. Error: " + e.Message);
                return GetUserInputForInt(message);
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


        private void ViewVehicles()
        {
            foreach (var vehicleInfo in _garageHandler.GetVehiclesInfo())
            {
                Console.WriteLine(vehicleInfo);
            }
        }
        private void ViewVehicleTypesWithCount()
        {
            Console.WriteLine(_garageHandler.GetVehicleTypesWithCount());
        }

        private void AddVehicleToGarage()
        {
            throw new NotImplementedException();
        }

        private void RemoveVehicleFromGarage()
        {
            throw new NotImplementedException();
        }

        private void FindVehiclesBasedOnProperties()
        {
            throw new NotImplementedException();
        }

        private void CreateNewGarage()
        {
            throw new NotImplementedException();
        }
    }
}

using Garage.Handlers;
using Garage.Models;
using Garage.Models.Vehicles;
using Garage.UI;

namespace Garage
{
    internal class Program
    {
        static void Main()
        {
            GarageHandler<Vehicle> garageHandler = new GarageHandler<Vehicle>();
            ConsoleUI<Vehicle> consoleUI = new ConsoleUI<Vehicle>(garageHandler);

            consoleUI.MainMenu();

        }
    }
}

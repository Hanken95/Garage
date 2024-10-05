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
            ConsoleUI.MainMenu(new GarageHandler());
        }
    }
}

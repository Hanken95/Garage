using Garage.Models;
using Garage.Models.Vehicles;

namespace Garage
{
    internal class Program
    {
        static void Main()
        {
            Garage<Vehicle> garage = new Garage<Vehicle>(4)
            {
                new Car(){ Colour = Colour.Purple , FuelType = FuelType.Diesel},
                new Boat(){ Length = 3.6 },
                new Motorcycle(){ CylinderVolume = 3}
            };

            foreach (var vehicle in garage)
            {
                Console.WriteLine(vehicle);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models.Vehicles
{
    public enum FuelType
    {
        Petrol,
        Diesel,
        Electric,
        Ethanol
    }

    public class Car : Vehicle
    {
        public FuelType FuelType { get; set; }

        public override string ToString()
        {
            return base.ToString() + $", Fueltype: {FuelType}";
        }
    }

}

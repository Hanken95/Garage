using Garage.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    public enum Colour
    {
        Red,
        Green, 
        Blue,
        Yellow,
        Pink,
        Purple,
        Orange,
        Black
    }
    public abstract class Vehicle : IVehicle
    {
        public int RegistrationNumber { get; set; } = 999;
        public Colour Colour { get; set; }
        public int Wheels { get; set; }



        public override string ToString()
        {
            return $"Registration number: {RegistrationNumber}, Wheels: {Wheels}, Colour: {Colour}";
        }
    }
}

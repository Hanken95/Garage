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
    public abstract class Vehicle
    {
        public int Passengers { get; set; }
        public int MaxPassengers { get; set; }
        public int RegistrationNumber { get; set; }
        public Colour Colour { get; set; }



        public override string ToString()
        {
            return $"Registration number: {RegistrationNumber}, Passengers: {Passengers}/{MaxPassengers}, Colour: {Colour}";
        }
    }
}

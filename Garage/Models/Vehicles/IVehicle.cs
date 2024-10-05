using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models.Vehicles
{
    public interface IVehicle
    {
        public int RegistrationNumber { get; set; }
        public Colour Colour { get; set; }
        public int Wheels { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models.Vehicles
{
    internal class Boat : Vehicle
    {
        /// <summary>
        /// Length in meters
        /// </summary>
        public double Length { get; set; }

        public override string ToString()
        {
            return base.ToString() + $", Lenght: {Length}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models.Vehicles
{
    public class Motorcycle : Vehicle
    {
        public double CylinderVolume { get; set; }

        public override string ToString()
        {
            return base.ToString() + $", cylinder volume: {CylinderVolume}";
        }
    }
}

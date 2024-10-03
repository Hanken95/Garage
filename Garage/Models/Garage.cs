using Garage.IEnumerators;
using Garage.Models.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    public class Garage<T>: IEnumerable<T> where T : Vehicle
    {

        private T[] vehicles;

        /// <summary>
        /// Returns how many vehicles can be stored in the garage
        /// </summary>
        public int Capacity
        {
            get { return vehicles.Length; }
        }

        public T this[int index]
        {
            get { return vehicles[index]; }
        }

        /// <summary>
        /// Returns how many vehicles are in the garage currently
        /// </summary>
        public int Count
        {
            get 
            {
                int count = 0;
                foreach (T vehicle in vehicles)
                {
                    if (vehicle != null)
                    {
                        count++;
                    }
                }
                return count;
            }
        }


        public Garage(int capacity)
        {
            vehicles = new T[capacity];
        }

        /// <summary>
        /// Adds a vehicle to the garage unless the vehicle is stored there already or the garage doesn't have any more space
        /// </summary>
        /// <param name="vehicle">The vehicle to be attempted to be stored in the garage</param>
        /// <returns>Returns true if the car was stored, returns false if not.</returns>
        public bool Add(T vehicle)
        {
            if (vehicles.Contains(vehicle))
            {
                return false;
            }
            else if (Capacity > Count)
            {
                for (int i = 0; i < Capacity; i++)
                {
                    if (vehicles[i] == null)
                    {
                        vehicles[i] = vehicle;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Removes a vehicle from the garage. Checks if the vehicle is in the garage first
        /// </summary>
        /// <param name="vehicle">The vehicle to be attempted to be removed from the garage</param>
        /// <returns>Returns true if the car was removed, returns false if not.</returns>
        public bool Remove(T vehicle)
        {
            if(vehicles.Contains(vehicle))
            {
                vehicle = null;
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new VehicleEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

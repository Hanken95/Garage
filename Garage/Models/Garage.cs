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
    public class Garage<T>: IEnumerable<T> where T : IVehicle
    {

        private T[] _vehicles;

        /// <summary>
        /// Returns how many vehicles can be stored in the garage
        /// </summary>
        public int Capacity
        {
            get { return _vehicles.Length; }
        }


        /// <summary>
        /// Returns how many vehicles are in the garage currently
        /// </summary>
        public int Count
        {
            get 
            {
                int count = 0;
                foreach (T vehicle in _vehicles)
                {
                    if (vehicle != null)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public T this[int index]
        {
            get { return _vehicles[index]; }
        }

        public bool IsFull { get { return Capacity <= Count; } }

        public Garage(int capacity)
        {
            _vehicles = new T[capacity];
        }

        /// <summary>
        /// Adds a vehicle to the garage unless the vehicle is stored there already or the garage doesn't have any more space
        /// </summary>
        /// <param name="vehicle">The vehicle to be attempted to be stored in the garage</param>
        /// <returns>Returns true if the car was stored, returns false if not.</returns>
        public bool Add(T vehicle)
        {
            if (Count >= Capacity || _vehicles.Contains(vehicle))
            {
                return false;
            }
            //if (_vehicles.GetType() == typeof(vehicle))
            //{
                for (int i = 0; i < Capacity; i++)
                {
                    if (_vehicles[i] == null)
                    {
                        _vehicles[i] = vehicle;
                        return true;
                    }
                }
            //}
            //if (_vehicles.GetType() != vehicle.GetType())
            //{
            //    return false;
            //}
            //switch (_vehicles)
            //{
            //    case Boat a:
                    
                    
                        
            //        for (int i = 0; i < Capacity; i++)
            //        {
            //            if (_vehicles[i] == null)
            //            {
            //                _vehicles[i] = b;
            //                return true;
            //            }
            //        }
                         
                    
            //        break;
            //    default:
            //        break;
            //}


            return false;
        }

        /// <summary>
        /// Removes a vehicle from the garage. Checks if the vehicle is in the garage first
        /// </summary>
        /// <param name="vehicle">The vehicle to be attempted to be removed from the garage</param>
        /// <returns>Returns true if the car was removed, returns false if not.</returns>
        public bool Remove(T vehicle)
        {
            if(_vehicles.Contains(vehicle))
            {
                vehicle = default;
                return true;
            }
            return false;
        }
        public bool Remove(int regNumber)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_vehicles[i] != null && _vehicles[i].RegistrationNumber == regNumber)
                {
                    _vehicles[i] = default;
                    return true;
                }
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

        public void Clear()
        {
            for (int i = 0; i < Capacity; i++)
            {
                _vehicles[i] = default;
            }
        }
    }
}

using Garage.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.IEnumerators
{
    internal class VehicleEnumerator<T> : IEnumerator<T> where T : Vehicle
    {
        private Garage<T> _garage;
        private int _index;

        public VehicleEnumerator(Garage<T> garage)
        {
            _garage = garage;
            _index = -1;
        }
        public T Current => _garage[_index];

        object IEnumerator.Current => Current;

        T IEnumerator<T>.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            return ++_index < _garage.Count;
        }

        public void Reset()
        {
            _index = -1;
        }
    }
}

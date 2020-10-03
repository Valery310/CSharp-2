using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employee
{
    /// <summary>
    /// класс, реализующий IEnumerator
    /// </summary>
    class Employes : IEnumerator<Employee>
    {
        Employee[] EmployesArray { get; set; }
        int Count { get; set; }
        int current { get; set; }
        public Employee Current => EmployesArray[current];
        object IEnumerator.Current => Current;

        public Employes(Employee[] employes)
        {
            EmployesArray = employes;
            Count = EmployesArray.Length - 1;
            current = -1;
        }

        public bool MoveNext()
        {
            if (current++ < Count)
            {
                return true;
            }
            Reset();
            return false;
        }

        public void Reset() => current = -1;

        public IEnumerator<Employee> GetEnumerator()
        {
            return (IEnumerator<Employee>)this;
        }

        public void Dispose()
        {
            EmployesArray = null;
            int Count = current = 0;
            GC.SuppressFinalize(this);
        }
    }
}

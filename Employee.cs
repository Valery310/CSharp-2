using System;
using System.Collections.Generic;
using System.Text;

namespace Employee
{
    /// <summary>
    /// Базовый абстрактный класс, реализующий IComparable.
    /// </summary>
    abstract class Employee:IComparable<Employee>
    {
        public string Name { get; private protected set; }
        public float AverageSalary { get; private protected set; }

        private protected Employee(string name)
        {
            Name = name;
          
        }

        private protected abstract void CalculateSalary();

        public int CompareTo(Employee obj)
        {
            if (this.AverageSalary > obj.AverageSalary)
            {
                return 1;
            }
            else if(this.AverageSalary == obj.AverageSalary)
            {
                return 0;
            }
            return -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    public class Employee
    {
        public string Name { get; set; }
        public int Salary { get; set; }

        public Employee(string name, int salary)
        {
            Name = name; 
            Salary = salary;
        }
    }
}

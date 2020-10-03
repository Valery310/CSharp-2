using System;
using System.Collections.Generic;
using System.Text;

namespace Employee
{
    /// <summary>
    /// Сотрудник с фиксированной оплатой.
    /// </summary>
    class EmpFixed: Employee
    {
        public float Salary { get; protected internal set; }

        public EmpFixed(string name, float salary) : base(name) { Salary = salary; CalculateSalary(); }

        private protected override void CalculateSalary()
        {
            base.AverageSalary = Salary;
        }
    }
}

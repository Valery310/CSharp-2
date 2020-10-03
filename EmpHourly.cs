using System;
using System.Collections.Generic;
using System.Text;

namespace Employee
{
    /// <summary>
    /// Сотрудник с почасовой оплатой.
    /// </summary>
    class EmpHourly : Employee
    {
        public float HourlyRate { get; private protected set; }

        public EmpHourly(string name, float hourlyRate) : base(name) { HourlyRate = hourlyRate; CalculateSalary(); }

        private protected override void CalculateSalary()
        {
            base.AverageSalary = 20.8F * 8 * HourlyRate;
        }
    }
}

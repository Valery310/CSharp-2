using System;

namespace Employee
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(Environment.TickCount);
            Employee[] employees = new Employee[60];

            for (int i = 0; i < 60; i++)//Заполнение массива сотрудников
            {
                if (i<30)
                {
                    employees[i] = new EmpFixed($"Сотудник с фиксированной оплатой {i}", random.Next(10000,150000));
                }
                else
                {
                    employees[i] = new EmpHourly($"Сотудник с почасовой оплатой {i}", random.Next(70, 1000));
                }
            }

            Array.Sort(employees);//сортировка по размеру среднемесячной зарплаты
            Employes employes = new Employes(employees);//класс, реализующий IEnumerator

            foreach (var item in employes)//вывод списка сотрудников с зарпалтами.
            {
                Console.WriteLine($"{item.Name,35} - {item.AverageSalary:f2}");
            }
        }
    }
}

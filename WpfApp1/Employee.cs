using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace WpfApp1
{
    public class Employee: INotifyPropertyChanged
    {
        public string _Name;
        public int _Salary;
        public Department _Department;
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged("Name"); } }
        public int Salary { get { return _Salary; } set { _Salary = value; OnPropertyChanged("Salary"); } }
        public Department Department { get { return _Department; } set { _Department = value; OnPropertyChanged("Department"); } }
        public static event EventHandler<EventArgsError> Error;

        public Employee(string name, int salary, Department department = null)
        {
            Name = name; 
            Salary = salary;
            Department = department;
        }

        public Employee()
        {
        }

        public void EditEmp(string name, int salary, Department department = null)
        {
            Name = name;
            Salary = salary;
            if (department!=null)
            {
              //  var temp = Department
                Department.RemoveEmp(this);
                Department = department;
                Department.AddEmp(this);
            }      
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static bool SaveEmp(Employee emp, string name, string salary, Department dep) 
        {
            if (emp != null)
            {
                int _salary = 0;
                if (!int.TryParse(salary, out _salary))
                {
                    Error(emp, new EventArgsError("Введите корректное значение зарплаты!"));
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Error(emp, new EventArgsError("Введите корректное имя!"));
                    }
                    else
                    {
                        if (dep == null)
                        {
                            Error(emp, new EventArgsError("Выберите подразделение!"));
                        }
                        else
                        {
                            emp.EditEmp(name, _salary, dep);
                            return true;
                        }
                    }
                }
            }
            else
            {
                Error(emp, new EventArgsError("Работник не выбран!"));
            }
            return false;
        }
    }
}

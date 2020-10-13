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

        public Employee(string name, int salary, Department department = null)
        {
            Name = name; 
            Salary = salary;
            Department = department;
        }

        public void EditEmp(string name, int salary, Department department = null)
        {
            Name = name;
            Salary = salary;
            if (department!=null)
            {
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
    }
}

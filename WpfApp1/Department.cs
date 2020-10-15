using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfApp1
{
    public class Department : INotifyPropertyChanged
    {
        public string _nameOfDepartment;
        public ObservableCollection<Employee> employees { get; set; }
        public string nameOfDepartment { get { return _nameOfDepartment; } set { _nameOfDepartment = value; OnPropertyChanged("nameOfDepartment"); } }
        public static event EventHandler<EventArgsError> Error;

        public Department(string name)
        {
            nameOfDepartment = name;
            employees = new ObservableCollection<Employee>();
        }

        public Department()
        {
            employees = new ObservableCollection<Employee>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void DepEdit(string NewName)
        {
            nameOfDepartment = NewName;
        }

        public void AddEmp(Employee employee)
        {
            employee.Department = this;
            employees.Add(employee);
        }

        public static void RemoveEmp(Employee employee)
        {
            employee.Department?.employees?.Remove(employee);
        }

        public static Department SaveDep(Department dep, string name)
        {
            if (dep != null)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    dep.nameOfDepartment = name;
                    return dep;
                }
                else
                {
                    Error(dep, new EventArgsError("Введите имя подразделения!"));
                }
            }
            else
            {
                Error(dep, new EventArgsError("Подразделение не выбрано!"));
            }
            return null;
        }
    }
}

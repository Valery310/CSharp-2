using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Web;

namespace Web_API.Models
{
    [DataContract(IsReference = true)]
    public class Department : INotifyPropertyChanged
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string _nameOfDepartment;
        [DataMember]
        public ObservableCollection<Employee> employees { get; set; }//= new ObservableCollection<Employee>();
        [DataMember]
        public string nameOfDepartment { get { return _nameOfDepartment; } set { _nameOfDepartment = value; OnPropertyChanged("nameOfDepartment"); } }
        public static event EventHandler<EventArgsError> Error;

        public Department(string name) : base()
        {
            nameOfDepartment = name;
            employees = new ObservableCollection<Employee>();
        }

        public Department()
        {
            employees = new ObservableCollection<Employee>();
        }

        public Department(int _id, string _name)
        {
            id = _id;
            nameOfDepartment = _name;
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
            DB.Edit(this);
        }

        public void AddEmp(Employee employee)
        {
            employee.Department = this;
            employees.Add(employee);
        }

        public static void RemoveEmp(Employee employee)
        {
            DB.Delete(employee);
            employee.Department?.employees?.Remove(employee);
            employee = null;
        }

        public static Department SaveDep(Department dep, string name)
        {
            if (dep != null)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    dep.DepEdit(name);
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
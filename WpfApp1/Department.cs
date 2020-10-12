using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WpfApp1
{
    public class Department
    {
        public ObservableCollection<Employee> employees { get; set; }
        public string nameOfDepartment { get; set; }

        public Department(string name) 
        {
            nameOfDepartment = name;
            employees = new ObservableCollection<Employee>();
        }

        public void AddEmp(Employee employee) 
        {
            employees.Add(employee);
        }

        public void EditEmp(Employee employee) 
        {
            
        }

        public void RemoveEmv(Employee employee) 
        {
            employees.Remove(employee);     
        }
    }
}

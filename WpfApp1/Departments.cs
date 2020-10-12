using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    public class Departments
    {
        public ObservableCollection<Department> departments { get; set; }

        public Departments()
        {
            departments = new ObservableCollection<Department>();
        }

        public void AddDep(Department department)
        {
            departments.Add(department);
        }

        public void EditDep(Department department)
        {

        }

        public void RemoveDep(Department department)
        {
            departments.Remove(department);
        }
    }
}

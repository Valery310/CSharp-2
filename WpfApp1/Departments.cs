using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    public class Departments
    {
        public ObservableCollection<Department> departments { get; set; }
        public static event EventHandler<EventArgsError> Error;

        public Departments()
        {
            departments = new ObservableCollection<Department>();
        }

        public bool AddDep(Department department)
        {
            if (department != null)
            {
                departments.Add(department);
                return true;
            }
            return false;
        }

        public void RemoveDep(Department department)
        {
            departments.Remove(department);
        }

        public static void AddObj(object obj, Departments departments) 
        {
            if (obj is Department)
            {
                var temDep = obj as Department;
                departments.RemoveDep(temDep);
            }
            if (obj is Employee)
            {
                var temEmp = obj as Employee;
                Department.RemoveEmp(temEmp);
            }
        }
    }
}

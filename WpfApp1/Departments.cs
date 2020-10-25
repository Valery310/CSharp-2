using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;

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

        public Departments(DataSet dataset)
        {
            departments = new ObservableCollection<Department>();
            try
            {
               GetData(dataset.Tables["Department"], dataset.Tables["Employees"]);    
            }
            catch (Exception ex)
            {
                Error(this, new EventArgsError(ex.Message));
            }
        }

        private void GetData(DataTable dep, DataTable emp)
        {
            departments.Clear();

            foreach (DataRow d in dep.Rows)
            {
                Department department = new Department((int)d[0], (string)d[1]);
                foreach (DataRow e in emp.Rows)
                {
                    Employee employee = new Employee((int)e[0], (string)e[1], department, (decimal)e[3]);
                    department.AddEmp(employee);
                }
                departments.Add(department);
            }

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

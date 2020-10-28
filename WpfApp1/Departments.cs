using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;

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
                MessageBox.Show(ex.Message);
             //   Error(this, new EventArgsError(ex.Message));
            }
        }

        private void GetData(DataTable dep, DataTable emp)
        {
           
            try
            {
                departments.Clear();
                foreach (DataRow d in dep.Rows)
                {
                    Department department = new Department((int)d[0], (string)d[1]);
                    var result = from row in emp.AsEnumerable()
                                 where row.Field<int>("id_department") == d.Field<int>("Id")
                                 select row;
                    foreach (DataRow e in result)
                    {
                        Decimal c = 0M;
                        try
                        {
                            c = (Decimal)e[3];
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        Employee employee = new Employee((int)e[0], (string)e[1], department, c);
                        department.AddEmp(employee);
                    }
                    departments.Add(department);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          

        }

        public bool AddDep(Department department)
        {
            if (department != null)
            {
                departments.Add(department);
                //DB.Insert(department);
                return true;
            }
            return false;
        }

        public void RemoveDep(Department department)
        {
            //DB.Delete(department);
            departments.Remove(department);
            department.employees.Clear();
            department = null;
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

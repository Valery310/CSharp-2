using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WpfApp1
{
    public static class Presenter
    {
        public static event EventHandler<EventArgsError> Error; //событие на случай ввода некорректных данных
        public static Departments departments { get; set; }

        public static void Load()
        {
            departments = new Departments(DB.FillData());
            Error += MainWindow_Error;
            Employee.Error += MainWindow_Error;
            Department.Error += MainWindow_Error;
        }

        private static void MainWindow_Error(object sender, EventArgsError e)
        {
            MessageBox.Show(e.message);
        }

        public static void LoadData()
        {
            DB.UpdateData();
        }

        public static bool SaveEmployee(Employee emp, string name, string salary, Department dep)
        {
            return Employee.SaveEmp(emp, name, salary, dep);
        }

        public static void AddedEmployee(Window window)
        {
            AddedNode addedNode = new AddedNode();
            addedNode.DataContext = window;
            addedNode.Owner = window;
            addedNode.Show();
        }

        public static void AddedDepartment(Window window)
        {
            AddedDep addedDep = new AddedDep();
            addedDep.Owner = window;
            addedDep.Show();
        }

        public static void Delete(object obj) 
        {
            Departments.AddObj(obj, departments);
        }

        public static bool SaveDepartment(string name)
        {
            return departments.AddDep(Department.SaveDep(new Department(), name));
        }
    }
}

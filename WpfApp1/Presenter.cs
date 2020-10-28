using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WpfApp1
{
    public static class Presenter
    {
        public static event EventHandler<EventArgsError> Error; //событие на случай ввода некорректных данных
        public delegate void LoadingHandler();
        public static event LoadingHandler Loading;
        public static Departments departments { get; set; }

        public async static void Load()
        {
            WebAPI.InitUri();
            departments = await WebAPI.GetDepatments();
            Error += MainWindow_Error;
            Employee.Error += MainWindow_Error;
            Department.Error += MainWindow_Error;
            Loading();
        }

        private static void MainWindow_Error(object sender, EventArgsError e)
        {
            MessageBox.Show(e.message);
        }

        public async static void LoadData()
        {
            departments = null;
            departments = await WebAPI.GetDepatments();
            Loading();
        }

        public static bool SaveEmployee(Employee emp, string name, string salary, Department dep)
        {
            return Employee.SaveEmp(emp, name, salary, dep);
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

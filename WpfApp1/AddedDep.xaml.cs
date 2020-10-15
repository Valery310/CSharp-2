using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddedDep.xaml
    /// </summary>
    public partial class AddedDep : Window
    {
        public static event EventHandler<EventArgsError> Error;
        Departments Departments;

        public AddedDep(Departments departments)
        {
            InitializeComponent();
            Departments = departments;
            Error += MainWindow_Error;
        }

        private void MainWindow_Error(object sender, EventArgsError e)
        {
            MessageBox.Show(e.message);
        }

        private void btnSaveDep_Click(object sender, RoutedEventArgs e)
        {                      
            if (Departments.AddDep(Department.SaveDep(new Department(), tbxNameDepartment.Text)))
            {
                this.Close();
            }              
        }
    }
}

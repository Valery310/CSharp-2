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
    /// Логика взаимодействия для AddedNode.xaml
    /// </summary>
    public partial class AddedNode : Window
    {
        public static event EventHandler<EventArgsError> Error;

        public AddedNode()
        {
            InitializeComponent();
            Error += MainWindow_Error;
        }

        private void MainWindow_Error(object sender, EventArgsError e)
        {
            MessageBox.Show(e.message);
        }

        private void btnSaveEmp_Click(object sender, RoutedEventArgs e)
        {
            int salary = 0;
            if (!int.TryParse(tbxSalary.Text, out salary))
            {
                Error(this, new EventArgsError("Введите корректное значение зарплаты!"));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(tbxNameEmployee.Text))
                {
                    Error(this, new EventArgsError("Введите корректное имя!"));
                }
                else
                {
                    if (cmbxDepartment.SelectedItem == null)
                    {
                        Error(this, new EventArgsError("Выберите подразделение!"));
                    }
                    else
                    {
                        var dep = cmbxDepartment.SelectedItem as Department;
                            dep.AddEmp(new Employee(tbxNameEmployee.Text, salary));
                        this.Close();

                    }
                }
            }
        }
    }
}

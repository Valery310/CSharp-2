using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static event EventHandler<EventArgsError> Error; //событие на случай ввода некорректных данных

        public Departments departments { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            departments = new Departments(DB.FillData());
            this.DataContext = this;//привязка контекста данных
            Error += MainWindow_Error;
            Employee.Error += MainWindow_Error;
            Department.Error += MainWindow_Error;


           // DB dB = new DB();
          //  DB.FillData();
            //tvDepartment.ItemsSource = DB.DepDataTable;
        //    dB.GetEmployes();
        //   dB.GetDepartments();
        }

        private void MainWindow_Error(object sender, EventArgsError e)
        {
            MessageBox.Show(e.message);
        }

        private void Load_Click(object sender, RoutedEventArgs e)//Загрузка тестовых данных
        {
            for (int i = 0; i < 10; i++)
            {
                Department department = new Department($"Подразделение {i}");
                for (int q = 0; q < 5; q++)
                {
                    department.AddEmp(new Employee($"Сотрудник {q}", q * 1000));
                }
                departments.AddDep(department);
            }                            
        }

        private void btnSaveEmp_Click(object sender, RoutedEventArgs e)//сохранение изменений по сотруднику
        {
            Employee.SaveEmp(tvDepartment?.SelectedItem as Employee, tbxNameEmployee.Text, tbxSalary.Text, cmbxDepartment?.SelectedItem as Department);
        }

        private void btnSaveDep_Click(object sender, RoutedEventArgs e)//сохранение изменений по подразделению
        {
            Department.SaveDep(tvDepartment?.SelectedItem as Department, tbxNameDepartment.Text);
        }

        private void tvDepartment_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)//событие выбора элементов списка
        {
            var temp = sender as TreeView;
            if (temp.SelectedItem is Department)
            {
                var temDep = temp.SelectedItem as Department;
                gbDep.IsEnabled = true;
                gbEmp.IsEnabled = false;
                tbxNameDepartment.Text = temDep.nameOfDepartment;
            }
            if (temp.SelectedItem is Employee)
            {
                var temEmp = temp.SelectedItem as Employee;
                gbDep.IsEnabled = false;
                gbEmp.IsEnabled = true;
                tbxNameEmployee.Text = temEmp.Name;
                tbxSalary.Text = temEmp.Salary.ToString();
                cmbxDepartment.SelectedItem = temEmp.Department;
            }        
        }

        private void btnAddEmp_Click(object sender, RoutedEventArgs e)//добавление нового сотрдуника
        {
            AddedNode addedNode = new AddedNode();
            addedNode.DataContext = this;          
            addedNode.Owner = this;
            addedNode.Show();
        }

        private void btnAddDep_Click(object sender, RoutedEventArgs e)
        {
            AddedDep addedDep = new AddedDep(departments);
            addedDep.Owner = this;
            addedDep.Show();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)//добавление нового подразделения/работника
        {
            Departments.AddObj(tvDepartment.SelectedItem, departments);
        }
    }
}

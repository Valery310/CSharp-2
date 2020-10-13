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
            departments = new Departments();
            tvDepartment.ItemsSource = departments.departments; //привязка списка к контролам
            cmbxDepartment.ItemsSource = departments.departments;
            Error += MainWindow_Error;
        }

        private void MainWindow_Error(object sender, EventArgsError e)
        {
            MessageBox.Show(e.message);
        }

        private void Load_Click(object sender, RoutedEventArgs e)//Загрузка тестовых данных
        {
            Department department1 = new Department("ИТ");
            department1.AddEmp(new Employee("Сотрудник ИТ 1", 1000));
            department1.AddEmp(new Employee("Сотрудник ИТ 2", 2000));
            department1.AddEmp(new Employee("Сотрудник ИТ 3", 3000));
            Department department2 = new Department("УМТС");
            department2.AddEmp(new Employee("Сотрудник УМТС 1", 4000));
            department2.AddEmp(new Employee("Сотрудник УМТС 2", 5000));
            department2.AddEmp(new Employee("Сотрудник УМТС 3", 6000));
            Department department3 = new Department("Бухгалтерия");
            department3.AddEmp(new Employee("Сотрудник Бухгалтерия 1", 7000));
            department3.AddEmp(new Employee("Сотрудник Бухгалтерия 2", 8000));

            departments.AddDep(department1);
            departments.AddDep(department2);
            departments.AddDep(department3);          
        }

        private void btnSaveEmp_Click(object sender, RoutedEventArgs e)//сохранение изменений по сотруднику
        {
            var emp = tvDepartment.SelectedItem as Employee;
            var dep = emp.Department;
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
                        emp.EditEmp(tbxNameEmployee.Text, salary, cmbxDepartment?.SelectedItem as Department);
                    }                  
                }                
            }             
        }

        private void btnSaveDep_Click(object sender, RoutedEventArgs e)//сохранение изменений по подразделению
        {
            var dep = tvDepartment?.SelectedItem as Department;
            if (!string.IsNullOrWhiteSpace(tbxNameDepartment.Text))
            {
                dep.nameOfDepartment = tbxNameDepartment.Text;
            }
            else
            {
                Error(this, new EventArgsError("Введите имя подразделения!"));
            }          
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
            addedNode.cmbxDepartment.ItemsSource = departments.departments;
            addedNode.Owner = this;
            addedNode.Show();
        }

        private void btnAddDep_Click(object sender, RoutedEventArgs e)
        {
            AddedDep addedDep = new AddedDep(departments);
            addedDep.Owner = this;
            addedDep.Show();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)//добавление нового подразделения
        {
            if (tvDepartment.SelectedItem is Department)
            {
                var temDep = tvDepartment.SelectedItem as Department;
                departments.RemoveDep(temDep);
            }
            if (tvDepartment.SelectedItem is Employee)
            {
                var temEmp = tvDepartment.SelectedItem as Employee;
                temEmp.Department.RemoveEmp(temEmp);
            }
        }
    }
}

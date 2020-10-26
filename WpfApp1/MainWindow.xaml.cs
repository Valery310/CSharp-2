using System;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {     
        public MainWindow()
        {
            InitializeComponent();        
            this.DataContext = this;//привязка контекста данных 
          
            Presenter.Load();
            tvDepartment.ItemsSource = Presenter.departments.departments;
            cmbxDepartment.ItemsSource = Presenter.departments.departments;

        }
       
        private void Load_Click(object sender, RoutedEventArgs e)//Загрузка данных
        {
            Presenter.LoadData();                        
        }

        private void btnSaveEmp_Click(object sender, RoutedEventArgs e)//сохранение изменений по сотруднику
        {
            Presenter.SaveEmployee(tvDepartment?.SelectedItem as Employee, tbxNameEmployee.Text, tbxSalary.Text, cmbxDepartment?.SelectedItem as Department);
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
            Presenter.AddedEmployee(this);
        }

        private void btnAddDep_Click(object sender, RoutedEventArgs e)
        {
            Presenter.AddedDepartment(this);
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)//добавление нового подразделения/работника
        {
            Presenter.Delete(tvDepartment.SelectedItem);
        }
    }
}

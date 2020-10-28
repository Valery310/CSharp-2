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
        }
       
        private void Load_Click(object sender, RoutedEventArgs e)//Загрузка данных
        {
            Presenter.LoadData();                        
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
                txtblckDepartment.Text = temEmp.Department.nameOfDepartment;
            }        
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Presenter.Loading += Presenter_Loading;
            Presenter.Load();
        }

        private void Presenter_Loading()
        {
            tvDepartment.ItemsSource = Presenter.departments.departments;
        }
    }
}

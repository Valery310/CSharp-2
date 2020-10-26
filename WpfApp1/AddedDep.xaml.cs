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
        public AddedDep()
        {
            InitializeComponent();
        }

        private void btnSaveDep_Click(object sender, RoutedEventArgs e)
        {         
           
            if (Presenter.SaveDepartment(tbxNameDepartment.Text))
            {
                this.Close();
            }              
        }
    }
}

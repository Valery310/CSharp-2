using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using WpfApp1.Properties;

namespace WpfApp1
{
    public class DB
    {
        SqlConnection connection = new SqlConnection(Settings.Default.connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet dataSet = new DataSet();

        public DataSet FillData() 
        {
           // string sqlExpression = @"SELECT Employees.FIO, Department.Department FROM Employees INNER JOIN Department ON (Employees.id_department = Department.Id) ORDER BY Employees.FIO ASC, Department.Department ASC;";
            string sqlEmp = "SELECT * FROM Employees";
            string sqlDep = "SELECT * FROM Department";
            
            adapter.SelectCommand = new SqlCommand(sqlEmp, connection);
            adapter.FillSchema(dataSet, SchemaType.Source);
            adapter.Fill(dataSet);
            adapter.SelectCommand = new SqlCommand(sqlDep, connection);
            adapter.FillSchema(dataSet, SchemaType.Source);
            adapter.Fill(dataSet);

            return dataSet;

            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            //  connection.CloseAsync();

            //  connection.CloseAsync();
            //  var t = dataSet;
            //foreach (DataColumn column in dt.Columns)
            //    Console.Write("\t{0}", column.ColumnName);
            //Console.WriteLine();
            //// перебор всех строк таблицы
            //foreach (DataRow row in dt.Rows)
            //{
            //    // получаем все ячейки строки
            //    var cells = row.ItemArray;
            //    foreach (object cell in cells)
            //        Console.Write("\t{0}", cell);
            //    Console.WriteLine();
            //}
        }

        public void UpdateData() 
        {
            adapter.Update(dataSet);
        }

        //SqlDataAdapter adapter = new SqlDataAdapter();
        //SqlCommand command = new SqlCommand("SELECT * FROM People", connection);
        //adapter.SelectCommand = command;
        //        // insert
        //        command = new SqlCommand(@"INSERT INTO People (FIO, Birthday, Email, Phone) VALUES (@FIO, @Birthday, @Email, @Phone); SET @ID = @@IDENTITY;", connection);
        //command.Parameters.Add("@FIO", SqlDbType.NVarChar, -1, "FIO");
        //        command.Parameters.Add("@Birthday", SqlDbType.NVarChar, -1, "Birthday");
        //        command.Parameters.Add("@Email", SqlDbType.NVarChar, 100, "Email");
        //        command.Parameters.Add("@Phone", SqlDbType.NVarChar, -1, "Phone");
        //        SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
        //param.Direction = ParameterDirection.Output;
        //        adapter.InsertCommand = command;
        //        // update
        //        command = new SqlCommand(@"UPDATE People SET FIO = @FIO, Birthday = @Birthday WHERE ID = @ID", connection);
        //command.Parameters.Add("@FIO", SqlDbType.NVarChar, -1, "FIO");
        //        command.Parameters.Add("@Birthday", SqlDbType.NVarChar, -1, "Birthday");
        //        param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
        //        param.SourceVersion = DataRowVersion.Original;
        //        adapter.UpdateCommand = command;
        //        // delete
        //        command = new SqlCommand("DELETE FROM People WHERE ID = @ID", connection);
        //param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
        //        param.SourceVersion = DataRowVersion.Original;



        #region sql
        private void GetTuples(string sqlExpression) 
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReaderAsync(CommandBehavior.CloseConnection).Result;
            connection.Close();
        }

        private void GetTuple(string sqlExpression, SqlParameter sqlParameter)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.Parameters.Add(sqlParameter);
            SqlDataReader reader = command.ExecuteReaderAsync(CommandBehavior.CloseConnection).Result;           
        }

        private void NewTuple(string sqlExpression)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            int exec = command.ExecuteNonQueryAsync().Result;
            if (exec == -1)
            {
                MessageBox.Show("Не выполнено.");
            }
        }

        private void EditTuple(string sqlExpression)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReaderAsync(CommandBehavior.CloseConnection).Result;
        }

        private void DelTuple(string sqlExpression)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReaderAsync(CommandBehavior.CloseConnection).Result;
        }

        private int GetIDTuples(string sqlExpression) 
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public void NewEmploye(Employee employee)
        {
            string sqlExpression = $"INSERT INTO Employees (FIO, id_department, Salary) VALUES ( N'{employee.Name}', '{GetIDTuples($"SELECT Id FROM Department WHERE Department = {employee.Department.nameOfDepartment}")}','{employee.Salary}')";
            NewTuple(sqlExpression);
        }

        public void NewDepartment(Department department)
        {
            string sqlExpression = $"INSERT INTO Department (Department) VALUES ( N'{department.nameOfDepartment}')";
            NewTuple(sqlExpression);
        }

        public void GetEmployee(Employee employee)
        {
            string sqlExpression = "SELECT * FROM Employees WHERE FIO = @Emp";
            SqlParameter param = new SqlParameter("@Emp", SqlDbType.NVarChar, -1);
            param.Value = $"{employee.Name}";
            GetTuple(sqlExpression, param);
        }
     
        public void GetDepartment(Department dep)
        {
            string sqlExpression = "SELECT * FROM Department where Department = @Dep";
            SqlParameter param = new SqlParameter("@Dep", SqlDbType.NVarChar, -1);
            param.Value = $"{dep.nameOfDepartment}";        
            GetTuple(sqlExpression, param);
        }
      
        public void GetEmployes() 
        {          
            string sqlExpression = "SELECT * FROM Employees";
            GetTuples(sqlExpression);
        }
      
        public void GetDepartments()
        {
            string sqlExpression = "SELECT * FROM Department";
            GetTuples(sqlExpression);
        }



        public void EditEmployee()
        { 
        
        }

        public void EditDepartment()
        { 
        
        }

        public void DelEmploye()
        {
        
        }

        public void DelDepartment()
        { 
        
        }
        #endregion sql
    }
}

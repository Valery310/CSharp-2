using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Properties;

namespace WpfApp1
{
    public static class DB
    {
        static SqlConnection connection = new SqlConnection(Settings.Default.connectionString);
        static SqlDataAdapter adapter = new SqlDataAdapter();
        public static DataSet dataSet = new DataSet();
        public static DataTable DepDataTable;
        public static DataTable EmpDataTable;
        static SqlCommandBuilder commandBuilder;
        public static DataView RootEmp { get; private set; }
        public static DataView RootDep { get; private set; }

        public static DataSet FillData() 
        {
           // string sqlExpression = @"SELECT * FROM Employees INNER JOIN Department ON (Employees.id_department = Department.Id) ORDER BY Employees.FIO ASC, Department.Department ASC;";
            string sqlEmp = "SELECT * FROM Employees; SELECT * FROM Department;";
          //  string sqlDep = "SELECT * FROM Department";           
            adapter.SelectCommand = new SqlCommand(sqlEmp, connection);
            adapter.FillSchema(dataSet, SchemaType.Mapped);
            adapter.Fill(dataSet);
            //adapter.SelectCommand = new SqlCommand(sqlDep, connection);
            //adapter.FillSchema(dataSet, SchemaType.Source);
            //adapter.Fill(dataSet);
            EmpDataTable = dataSet.Tables[0];           
            EmpDataTable.TableName = "Employees";
            //EmpDataTable.Columns[0].ColumnName = "Id";
            //EmpDataTable.Columns[1].ColumnName = "FIO";
            //EmpDataTable.Columns[2].ColumnName = "id_department";
            //EmpDataTable.Columns[3].ColumnName = "Salary";

            DepDataTable = dataSet.Tables[1];
            DepDataTable.TableName = "Department";
            //DepDataTable.Columns[0].ColumnName = "Id";
            //DepDataTable.Columns[1].ColumnName = "Department";
            RootEmp = new DataView(EmpDataTable);
            RootDep = new DataView(DepDataTable);
            dataSet.Relations.Add("DepToEmp", DepDataTable.Columns["Id"], EmpDataTable.Columns["id_department"]);
            commandBuilder = new SqlCommandBuilder(adapter);
            return dataSet;
        }

      /*  public static void UpdateAllData() 
        {           
            adapter.Update(dataSet);
            UpdateData();
        }

        public static void UpdateEmpData()
        {
            adapter.Update(EmpDataTable);
            UpdateData();
        }

        public static void UpdateDepData()
        {
            adapter.Update(DepDataTable);
            UpdateData();
        }

        private static void UpdateData() 
        {
            dataSet.AcceptChanges();
            dataSet.Clear();
            FillData();
        }*/



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



     /*   #region sql
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
        #endregion sql*/
    }
}

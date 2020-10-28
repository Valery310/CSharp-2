using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Web_API.Properties;

namespace Web_API.Models
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
        public static event EventHandler<EventArgsError> Error;

        public static DataSet FillData()
        {
            string sqlEmp = "SELECT * FROM Employees; SELECT * FROM Department;";

            adapter.SelectCommand = new SqlCommand(sqlEmp, connection);
            adapter.FillSchema(dataSet, SchemaType.Mapped);
            adapter.Fill(dataSet);

            EmpDataTable = dataSet.Tables[0];
            EmpDataTable.TableName = "Employees";


            DepDataTable = dataSet.Tables[1];
            DepDataTable.TableName = "Department";

            RootEmp = new DataView(EmpDataTable);
            RootDep = new DataView(DepDataTable);
            dataSet.Relations.Add("DepToEmp", DepDataTable.Columns["Id"], EmpDataTable.Columns["id_department"]);
            commandBuilder = new SqlCommandBuilder(adapter);
            return dataSet;
        }

        public static void UpdateAllData()
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

        public static void UpdateData()
        {
            dataSet.Clear();
            adapter.Fill(dataSet);
        }

        public static void Insert(Employee emp)
        {
            string sqlExpression = $"INSERT INTO Employees (FIO, id_department, Salary) output INSERTED.ID VALUES ( N'{emp.Name}', '{emp.Department.id}', '{emp.Salary}')";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            int id = Convert.ToInt32(command.ExecuteScalarAsync().Result);
            if (id == 0)
            {
                Error(emp, new EventArgsError("Не выполнено."));
            }
            else
            {
                emp.id = id;
            }
            connection.Close();
        }

        public static void Insert(Department dep)
        {
            string sqlExpression = $"INSERT INTO Department (Department) output INSERTED.ID VALUES ( N'{dep.nameOfDepartment}')";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            int id = Convert.ToInt32(command.ExecuteScalarAsync().Result);
            if (id == 0)
            {
                Error(dep, new EventArgsError("Не выполнено."));
            }
            else
            {
                dep.id = id;
            }
            connection.Close();
        }

        public static void Delete(Employee emp)
        {
            string sqlExpression = $"DELETE FROM Employees WHERE Id = '{emp.id}'";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            int exec = Convert.ToInt32(command.ExecuteNonQueryAsync().Result);
            if (exec == -1)
            {
                Error(emp, new EventArgsError("Не выполнено."));
            }
            connection.Close();
        }

        public static void Delete(Department dep)
        {
            string sqlExpression = $"DELETE FROM Department WHERE Id = '{dep.id}'";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            int exec = Convert.ToInt32(command.ExecuteNonQueryAsync().Result);
            if (exec == -1)
            {
                Error(dep, new EventArgsError("Не выполнено."));
            }
            connection.Close();
        }

        public static void Edit(Employee emp)
        {
            string sqlExpression = $"UPDATE Employees SET FIO = ( N'{emp.Name}'), id_department = '{emp.Department.id}', Salary = '{emp.Salary}' WHERE Id = '{emp.id}'";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            int exec = Convert.ToInt32(command.ExecuteNonQueryAsync().Result);
            if (exec == -1)
            {
                Error(emp, new EventArgsError("Не выполнено."));
            }
            connection.Close();
        }

        public static void Edit(Department dep)
        {
            string sqlExpression = $"UPDATE Department SET Department = ( N'{dep.nameOfDepartment}') WHERE Id = '{dep.id}'";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            int exec = Convert.ToInt32(command.ExecuteNonQueryAsync().Result);
            if (exec == -1)
            {
                Error(dep, new EventArgsError("Не выполнено."));
            }
            connection.Close();
        }
    }
}
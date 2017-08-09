using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace MVCTest.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DeptID { get; set; }
        public bool Status { get; set; }

        

        static string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public static int InsertUpdateEmployee(Employee employee)
        {            
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("USP_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", employee.ID);
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@deptID", employee.DeptID);
                cmd.Parameters.AddWithValue("@status", employee.Status);
                cmd.Parameters.AddWithValue("@type", "IUEMP");
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static List<Employee> GetEmployee()
        {
            List<Employee> lstEmployee = new List<Employee>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("USP_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@type", "SELECTEMP");
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt32(rdr["ID"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.DeptID = Convert.ToInt32(rdr["DeptID"]);
                    employee.Status = Convert.ToBoolean(rdr["Status"]);
                    lstEmployee.Add(employee);
                }
            }

            return lstEmployee;
        }

        public static Employee GetEmployee(int id)
        {
            Employee employee = new Employee();
            using (SqlConnection con = new SqlConnection(cs))
            {                
                SqlCommand cmd = new SqlCommand("USP_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@type", "EMPBYID");
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {                    
                    employee.ID = Convert.ToInt32(rdr["ID"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.DeptID = Convert.ToInt32(rdr["DeptID"]);
                    employee.Status = Convert.ToBoolean(rdr["Status"]);
                }
            }

            return employee;
        }
    }
}
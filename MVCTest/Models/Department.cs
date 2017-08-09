using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;

namespace MVCTest.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static List<SelectListItem> GetDepartment()
        {
            List<SelectListItem> lstDepartment = new List<SelectListItem>();
            lstDepartment.Add(new SelectListItem() { Text = "--- Select Department ---", Value = "" });
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from Department", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lstDepartment.Add(new SelectListItem
                    {
                        Text = rdr["Name"].ToString(),
                        Value = rdr["ID"].ToString()
                    });
                }
            }
            return lstDepartment;
        }
    }
}
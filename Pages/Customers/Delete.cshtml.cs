using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CRUD.Pages.Customers
{
    public class Delete : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost(int id) {
            DeleteCustomer(id);
            Response.Redirect("/Customers/Index");
        }

        private void DeleteCustomer(int id) {
            try {
                string connectionString = "Server=LAPTOP-N6PCLAVT\\SQLEXPRESS01;Database=crmDB;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new(connectionString)) {
                    connection.Open();

                    string sqlQuery = "DELETE FROM customers WHERE id=@id";

                    using(SqlCommand command = new(sqlQuery, connection)) {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            } catch(System.Exception ex) {
                Console.WriteLine("Error: ", ex.Message);
                return;
            }
        }
    }
}
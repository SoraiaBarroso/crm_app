using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CRUD.Pages.Customers
{
    public class Create : PageModel
    {
        [BindProperty, Required(ErrorMessage = "First Name is required")]
        public string Firstname {get; set;} = "";
        
        [BindProperty, Required(ErrorMessage = "Last Name is requireed")]
        public string Lastname {get; set;} = "";
        
        [BindProperty, Required, EmailAddress]
        public string Email {get; set;} = "";
        
        [BindProperty, Phone]
        public string? Phone {get; set;} 
        
        [BindProperty]
        public string? Address {get; set;} 
        
        [BindProperty, Required(ErrorMessage = "Company name is required")]
        public string Company {get; set;} = "";

        [BindProperty]
        public string? Notes {get; set;} 

        public string ErrorMessage {get; set;} = "";

        public void OnGet(){}

        public void OnPost()
        {
            if (!ModelState.IsValid) {
                return;
            }

            // if they are null asign ""
            Phone ??= "";
            Address ??= "";
            Notes ??= "";

            try {
                string connectionString = "Server=LAPTOP-N6PCLAVT\\SQLEXPRESS01;Database=crmDB;Trusted_Connection=True;TrustServerCertificate=True;";

                SqlConnection connection = new(connectionString);
                connection.Open();

                string sqlQuery = "INSERT INTO customers " + 
                                   "(firstname, lastname, email, phone, address, company, notes) " +
                                   "VALUES (@firstname, @lastname, @email, @phone, @address, @company, @notes);";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@firstname", Firstname);
                    command.Parameters.AddWithValue("@lastname", Lastname);
                    command.Parameters.AddWithValue("@email", Email);
                    command.Parameters.AddWithValue("@phone", Phone);
                    command.Parameters.AddWithValue("@address", Address);
                    command.Parameters.AddWithValue("@company", Company);
                    command.Parameters.AddWithValue("@notes", Notes);

                    command.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex) {
                ErrorMessage = ex.Message;
                Console.WriteLine("Error: ", ex.Message);
                return;
            }

            // redirect user
            Response.Redirect("/Customers/Index");
        }
    }
}
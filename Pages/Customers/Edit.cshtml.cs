using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CRUD.Pages.Customers
{
    public class Edit : PageModel
    {
        [BindProperty]
        public int Id {get; set;}

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

        // get user data
        public void OnGet(int id)
        {
            Console.WriteLine(id);

            try
            {
                string connectionString = "Server=LAPTOP-N6PCLAVT\\SQLEXPRESS01;Database=crmDB;Trusted_Connection=True;TrustServerCertificate=True;";
                
                using (SqlConnection connection = new(connectionString)) {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM customers WHERE id=@id";

                    using (SqlCommand command = new(sqlQuery, connection)){
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader()){
                            if (reader.Read())
                            {
                                Id = reader.GetInt32(0);
                                Firstname = reader.GetString(1);
                                Lastname = reader.GetString(2);
                                Email = reader.GetString(3);
                                Phone = reader.GetString(4);
                                Address = reader.GetString(5);
                                Company = reader.GetString(6);
                                Notes = reader.GetString(7);
                            }
                            else
                            {
                                Response.Redirect("/Customers/Index");
                            }
                        }
                    }
                }             
            }
            catch (System.Exception ex)
            {
                ErrorMessage = ex.Message;
                Console.WriteLine("Error: " + ErrorMessage);
                return;
            }
        }

        public void OnPost() {
            if (!ModelState.IsValid) {
                return;
            }

            // if they are null asign ""
            Phone ??= "";
            Address ??= "";
            Notes ??= "";

            // update customer details
            try {
                string connectionString = "Server=LAPTOP-N6PCLAVT\\SQLEXPRESS01;Database=crmDB;Trusted_Connection=True;TrustServerCertificate=True;";

                SqlConnection connection = new(connectionString);
                connection.Open();

                string sqlQuery = "UPDATE customers SET firstname=@firstname, lastname=@lastname, email=@email, " +
                                  "phone=@phone, address=@address, company=@company, notes=@notes WHERE id=@id";

                SqlCommand command = new(sqlQuery, connection);

                command.Parameters.AddWithValue("@firstname", Firstname);
                command.Parameters.AddWithValue("@lastname", Lastname);
                command.Parameters.AddWithValue("@email", Email);
                command.Parameters.AddWithValue("@phone", Phone);
                command.Parameters.AddWithValue("@address", Address);
                command.Parameters.AddWithValue("@company", Company);
                command.Parameters.AddWithValue("@notes", Notes);
                command.Parameters.AddWithValue("@id", Id);

                command.ExecuteNonQuery();
            }
            catch (System.Exception ex) {
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Customers/Index");
        }
    }
}
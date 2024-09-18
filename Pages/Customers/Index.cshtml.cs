using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CRUD.Pages.Customers
{
    public class Index : PageModel
    {
        // list of customers
        public List<CustomerInfo> CustomerList {get; set;} = [];
        public void OnGet()
        {
            // conect to DB and read customers
            try
            {
                string connectionString = "Server=.;Database=crmDB;Trusted_Connection=True;TrustServerCertificate=True;";
                string sqlCommand = "SELECT * FROM customers ORDER BY id DESC";

                // object of type SqlConnection
                using SqlConnection connection = new(connectionString);

                connection.Open();

                // create command
                using SqlCommand command = new(sqlCommand, connection);
                // create reader
                using SqlDataReader reader = command.ExecuteReader();
                // read row
                while (reader.Read()) {
                    CustomerInfo customerInfo = new();
                    customerInfo.Id = reader.GetInt32(0);
                    customerInfo.FirstName = reader.GetString(1);
                    customerInfo.LastName = reader.GetString(2);
                    customerInfo.Email = reader.GetString(3);
                    customerInfo.Phone = reader.GetString(4);
                    customerInfo.Address = reader.GetString(5);
                    customerInfo.Company = reader.GetString(6);
                    customerInfo.Notes = reader.GetString(7);
                    customerInfo.CreatedAt = reader.GetDateTime(8).ToString("MM/dd/yyyy");

                    // add each customer to the list
                    CustomerList.Add(customerInfo);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    public class CustomerInfo {
        public int Id {get; set;}
        public string FirstName {get; set;} = "";
        public string LastName {get; set;} = "";
        public string Email {get; set;} = "";
        public string Phone {get; set;} = "";
        public string Address {get; set;} = "";
        public string Company {get; set;} = "";
        public string Notes {get; set;} = "";
        public string CreatedAt {get; set;} = "";
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CRUD.Pages.Customers
{
    public class Create : PageModel
    {
        [BindProperty, Required(ErrorMessage = "First Name is required")]
        public string Firstname {get; set;} = "";
        
        [BindProperty, Required(ErrorMessage = "Last Name is requireed")]
        public string Lastname {get; set;} = "";
        
        [BindProperty, Required(ErrorMessage = "Email is required")]
        public string Email {get; set;} = "";
        
        [BindProperty]
        public string? Phone {get; set;} 
        
        [BindProperty]
        public string? Address {get; set;} 
        
        [BindProperty, Required(ErrorMessage = "Company name is required")]
        public string Company {get; set;} = "";

        [BindProperty]
        public string? Notes {get; set;} 

        public void OnGet()
        {
        }
    }
}
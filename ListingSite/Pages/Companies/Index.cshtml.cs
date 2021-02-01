using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listing.Models;
using Listing.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ListingSite.Pages.Companies
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;

        public IEnumerable<Employee> Employees { get; set; }

        public IndexModel(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public void OnGet()
        {
            Employees = employeeRepository.GetAllEmployees();
        }
    }
}

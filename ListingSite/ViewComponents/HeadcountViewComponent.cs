using Listing.Models;
using Listing.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListingSite.ViewComponents
{
    public class HeadcountViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository employeeRepository;

        public HeadcountViewComponent(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IViewComponentResult Invoke(Dept? department = null)
        {
            var result = employeeRepository.EmployeeCountByDept(department);
            return View(result);
        }
    }
}

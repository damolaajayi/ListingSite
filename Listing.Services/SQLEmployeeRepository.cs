using Listing.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Listing.Services
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public SQLEmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee newEmployee)
        {
            context.Database.ExecuteSqlRaw("spInsertEmployee {0}, {1}, {2}, {3}",
                                    newEmployee.Name,
                                    newEmployee.Email,
                                    newEmployee.PhotoPath,
                                    newEmployee.Department);
            return newEmployee;
        }

        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            if(employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = context.Employees;
            if (dept.HasValue)
            {
                query = query.Where(e => e.Department == Dept.Value);
            }
            return query.GroupBy(e => e.Department)
                                .Select(g => new DeptHeadCount()
                                {
                                    Department = g.Key.Value,
                                    Count = g.Count()
                                }).ToList();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees
                    .FromSqlRaw<Employee>("SELECT * FROM Employees")
                    .ToList();
        }

        public Employee GetEmployee(int id)
        {
            SqlParameter parameter = new SqlParameter("@Id", id);

            return context.Employees
                    .FromSqlRaw<Employee>("spGetEmployeeById @Id", parameter)
                    .ToList()
                    .FirstOrDefault();
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Employees;
            }
            return context.Employees.Where(e => e.Name.Contains(searchTerm) ||
                                e.Email.Contains(searchTerm)).ToList();
        }

        public Employee Update(Employee updatedEmployee)
        {
            var employee = context.Employees.Attach(updatedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedEmployee;
        }
    }
}

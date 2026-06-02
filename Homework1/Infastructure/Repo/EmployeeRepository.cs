using Homework1.Applicationss.Interfaces;
using Homework1.Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Infastructure.Repo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAllOrderedByName()
        {
            return _context.Employees.OrderBy(e => e.FullName).ToList();
        }
    }
}

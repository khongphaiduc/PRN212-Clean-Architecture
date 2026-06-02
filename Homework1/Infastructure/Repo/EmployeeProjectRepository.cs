using Homework1.Applicationss.Interfaces;
using Homework1.Infastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Infastructure.Repo
{
    public class EmployeeProjectRepository : IEmployeeProjectRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeProjectRepository(CompanyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeProject> GetByProjectId(Guid projectId)
        {
            return _context.EmployeeProjects
                .Include(ep => ep.Employee)
                .Where(ep => ep.ProjectId == projectId)
                .ToList();
        }

        public async Task<bool> ExistsAsync(Guid projectId, Guid employeeId)
        {
            return await _context.EmployeeProjects
                .AnyAsync(ep => ep.ProjectId == projectId && ep.EmployeeId == employeeId);
        }

        public async Task AddAsync(EmployeeProject employeeProject)
        {
            await _context.EmployeeProjects.AddAsync(employeeProject);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

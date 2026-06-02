using Homework1.Applicationss.Interfaces;
using Homework1.Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Infastructure.Repo
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly CompanyDbContext _context;

        public ProjectRepository(CompanyDbContext context)
        {
            _context = context;
        }

        public async Task AddEmployeeToProjectAsync(Guid projectId, Guid employeeId, string role)
        {
            var newEmpProj = new EmployeeProject
            {
                ProjectId = projectId,
                EmployeeId = employeeId,
                RoleInProject = string.IsNullOrEmpty(role) ? "Thành viên" : role
            };

            await _context.EmployeeProjects.AddAsync(newEmpProj);
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.ToList();
        }

        public Project GetById(Guid id)
        {
            return _context.Projects.Find(id);
        }
    }
}

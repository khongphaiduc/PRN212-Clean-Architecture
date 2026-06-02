using Homework1.Applicationss.DTOs;
using Homework1.Applicationss.Interfaces;
using Homework1.Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework1.Infastructure.ServiceImplement
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IEmployeeProjectRepository _empProjectRepo;

        public ProjectService(
            IProjectRepository projectRepo,
            IEmployeeRepository employeeRepo,
            IEmployeeProjectRepository empProjectRepo)
        {
            _projectRepo = projectRepo;
            _employeeRepo = employeeRepo;
            _empProjectRepo = empProjectRepo;
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            return _projectRepo.GetAll().Select(p => new ProjectDTO
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                StartDate = p.StartDate,
                EndDate = p.EndDate
            }).ToList();
        }

        public IEnumerable<EmployeeDTO> GetAllEmployeesOrderedByName()
        {
            return _employeeRepo.GetAllOrderedByName().Select(e => new EmployeeDTO
            {
                EmployeeId = e.EmployeeId,
                FullName = e.FullName,
                Email = e.Email
            }).ToList();
        }

        public IEnumerable<EmployeeProjectDTO> GetEmployeesInProject(Guid projectId)
        {
            return _empProjectRepo.GetByProjectId(projectId).Select(ep => new EmployeeProjectDTO
            {
                EmployeeId = ep.EmployeeId,
                EmployeeName = ep.Employee?.FullName ?? "N/A", 
                EmployeeEmail  = ep.Employee?.Email,
                ProjectId = ep.ProjectId,
                RoleInProject = ep.RoleInProject
            }).ToList();
        }

        public async Task<bool> AddEmployeeToProjectAsync(Guid projectId, Guid employeeId, string role)
        {
            bool isExist = await _empProjectRepo.ExistsAsync(projectId, employeeId);
            if (isExist)
            {
                return false;
            }

            var newEmpProj = new EmployeeProject
            {
                ProjectId = projectId,
                EmployeeId = employeeId,
                RoleInProject = string.IsNullOrEmpty(role) ? "Thành viên" : role
            };

            await _empProjectRepo.AddAsync(newEmpProj);
            await _empProjectRepo.SaveChangesAsync();

            return true;
        }
    }
}
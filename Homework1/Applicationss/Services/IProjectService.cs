using Homework1.Applicationss.DTOs;
using Homework1.Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework1.Applicationss.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDTO> GetAllProjects();
        IEnumerable<EmployeeDTO> GetAllEmployeesOrderedByName();
        IEnumerable<EmployeeProjectDTO> GetEmployeesInProject(Guid projectId);
        Task<bool> AddEmployeeToProjectAsync(Guid projectId, Guid employeeId, string role);
    }
}
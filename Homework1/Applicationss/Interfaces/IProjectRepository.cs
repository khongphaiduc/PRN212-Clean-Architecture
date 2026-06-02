using Homework1.Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Applicationss.Interfaces
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        Project GetById(Guid id);

        Task AddEmployeeToProjectAsync(Guid projectId, Guid employeeId, string role);
    }
}

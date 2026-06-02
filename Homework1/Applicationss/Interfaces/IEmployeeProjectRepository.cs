using Homework1.Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Applicationss.Interfaces
{
    public interface IEmployeeProjectRepository
    {
        IEnumerable<EmployeeProject> GetByProjectId(Guid projectId);
        Task<bool> ExistsAsync(Guid projectId, Guid employeeId);
        Task AddAsync(EmployeeProject employeeProject);
        Task SaveChangesAsync();
    }
}

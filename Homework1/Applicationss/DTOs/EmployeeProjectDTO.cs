using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Applicationss.DTOs
{
    public class EmployeeProjectDTO
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string? EmployeeEmail { get; set; }
        public Guid ProjectId { get; set; }
        public string? RoleInProject { get; set; }
    }
}

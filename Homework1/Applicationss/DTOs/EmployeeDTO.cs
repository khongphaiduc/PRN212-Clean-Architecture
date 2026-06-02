using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Applicationss.DTOs
{
    public class EmployeeDTO
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
    }
}

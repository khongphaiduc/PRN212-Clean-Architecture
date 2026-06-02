using System;
using System.Collections.Generic;

namespace Homework1.Infastructure.Models;

public partial class EmployeeProject
{
    public Guid EmployeeId { get; set; }

    public Guid ProjectId { get; set; }

    public string? RoleInProject { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}

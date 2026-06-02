using System;
using System.Collections.Generic;

namespace Homework1.Infastructure.Models;

public partial class Project
{
    public Guid ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
}

using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using MyCrm.ServiceCategories;

namespace MyCrm.Services;

public class Service : AuditedAggregateRoot<int>
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Recurring { get; set; }

    public string? Icon { get; set; }

    public string? Notes { get; set; }

    public int? ServiceCategoryId { get; set; }

    public ServiceCategory? ServiceCategory { get; set; }
}

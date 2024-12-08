using System;
using Volo.Abp.Application.Dtos;
using MyCrm.ServiceCategories;

namespace MyCrm.Services;

public class ServiceDto : AuditedEntityDto<int>
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Recurring { get; set; }

    public string? Icon { get; set; }

    public string? Notes { get; set; }

    public ServiceCategoryDto? ServiceCategory { get; set; }
}

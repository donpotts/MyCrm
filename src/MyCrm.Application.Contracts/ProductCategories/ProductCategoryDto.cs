using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.ProductCategories;

public class ProductCategoryDto : AuditedEntityDto<int>
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public string? TaxRate { get; set; }

    public string? Notes { get; set; }
}

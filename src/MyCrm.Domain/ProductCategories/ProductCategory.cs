using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyCrm.ProductCategories;

public class ProductCategory : AuditedAggregateRoot<int>
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public string? TaxRate { get; set; }

    public string? Notes { get; set; }
}

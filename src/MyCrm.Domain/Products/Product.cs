using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using MyCrm.ProductCategories;

namespace MyCrm.Products;

public class Product : AuditedAggregateRoot<int>
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public int StockQuantity { get; set; }

    public string? Photo { get; set; }

    public string? Notes { get; set; }

    public int? ProductCategoryId { get; set; }

    public ProductCategory? ProductCategory { get; set; }
}

using System;

namespace MyCrm.Services;

public class CreateUpdateServiceDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Recurring { get; set; }

    public string? Icon { get; set; }

    public string? Notes { get; set; }

    public int? ServiceCategoryId { get; set; }
}

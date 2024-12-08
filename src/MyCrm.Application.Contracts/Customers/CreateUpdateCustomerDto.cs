using System;

namespace MyCrm.Customers;

public class CreateUpdateCustomerDto
{
    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Industry { get; set; }

    public string? Logo { get; set; }

    public string? Notes { get; set; }

    public int? AddressId { get; set; }

    public int? ContactId { get; set; }
}

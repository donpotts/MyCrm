using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using MyCrm.Addresses;
using MyCrm.Products;
using MyCrm.Services;

namespace MyCrm.Vendors;

public class Vendor : AuditedAggregateRoot<int>
{
    public string? Name { get; set; }

    public string? ContactName { get; set; }

    public int Phone { get; set; }

    public string? Email { get; set; }

    public string? Logo { get; set; }

    public string? Notes { get; set; }

    public int? AddressId { get; set; }

    public Address? Address { get; set; }

    public int? ServiceId { get; set; }

    public Service? Service { get; set; }

    public int? ProductId { get; set; }

    public Product? Product { get; set; }
}

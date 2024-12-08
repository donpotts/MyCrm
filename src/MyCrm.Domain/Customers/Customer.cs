using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using MyCrm.Addresses;
using MyCrm.Contacts;

namespace MyCrm.Customers;

public class Customer : AuditedAggregateRoot<int>
{
    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Industry { get; set; }

    public string? Logo { get; set; }

    public string? Notes { get; set; }

    public int? AddressId { get; set; }

    public Address? Address { get; set; }

    public int? ContactId { get; set; }

    public Contact? Contact { get; set; }
}

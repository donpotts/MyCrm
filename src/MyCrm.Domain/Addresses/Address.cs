using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyCrm.Addresses;

public class Address : AuditedAggregateRoot<int>
{
    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public int ZipCode { get; set; }

    public string? Country { get; set; }

    public string? Photo { get; set; }

    public string? Notes { get; set; }
}

using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using MyCrm.Addresses;
using MyCrm.Contacts;
using MyCrm.Opportunities;

namespace MyCrm.Leads;

public class Lead : AuditedAggregateRoot<int>
{
    public string? Source { get; set; }

    public string? Status { get; set; }

    public double PotentialValue { get; set; }

    public string? Photo { get; set; }

    public string? Notes { get; set; }

    public int? AddressId { get; set; }

    public Address? Address { get; set; }

    public int? ContactId { get; set; }

    public Contact? Contact { get; set; }

    public int? OpportunityId { get; set; }

    public Opportunity? Opportunity { get; set; }
}

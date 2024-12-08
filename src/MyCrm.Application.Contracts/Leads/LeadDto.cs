using System;
using Volo.Abp.Application.Dtos;
using MyCrm.Addresses;
using MyCrm.Contacts;
using MyCrm.Opportunities;

namespace MyCrm.Leads;

public class LeadDto : AuditedEntityDto<int>
{
    public string? Source { get; set; }

    public string? Status { get; set; }

    public double PotentialValue { get; set; }

    public string? Photo { get; set; }

    public string? Notes { get; set; }

    public AddressDto? Address { get; set; }

    public ContactDto? Contact { get; set; }

    public OpportunityDto? Opportunity { get; set; }
}

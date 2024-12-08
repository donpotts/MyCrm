using System;

namespace MyCrm.Leads;

public class CreateUpdateLeadDto
{
    public string? Source { get; set; }

    public string? Status { get; set; }

    public double PotentialValue { get; set; }

    public string? Photo { get; set; }

    public string? Notes { get; set; }

    public int? AddressId { get; set; }

    public int? ContactId { get; set; }

    public int? OpportunityId { get; set; }
}

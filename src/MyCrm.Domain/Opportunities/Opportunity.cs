using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyCrm.Opportunities;

public class Opportunity : AuditedAggregateRoot<int>
{
    public DateTime EstimatedCloseDate { get; set; }

    public string? Stage { get; set; }

    public string? Icon { get; set; }

    public string? Notes { get; set; }
}

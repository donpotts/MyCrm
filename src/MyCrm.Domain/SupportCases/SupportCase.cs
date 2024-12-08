using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyCrm.SupportCases;

public class SupportCase : AuditedAggregateRoot<int>
{
    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public string? ServiceId { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime ModifiedDateTime { get; set; }

    public int UserId { get; set; }

    public string? FollowupDate { get; set; }

    public string? Icon { get; set; }

    public string? Notes { get; set; }
}

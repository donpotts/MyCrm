using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.Opportunities;

public class OpportunityDto : AuditedEntityDto<int>
{
    public DateTime EstimatedCloseDate { get; set; }

    public string? Stage { get; set; }

    public string? Icon { get; set; }

    public string? Notes { get; set; }
}

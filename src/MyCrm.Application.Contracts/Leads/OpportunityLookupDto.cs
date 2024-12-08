using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.Leads;

public class OpportunityLookupDto : EntityDto<int>
{
	public string? Stage { get; set; }
}

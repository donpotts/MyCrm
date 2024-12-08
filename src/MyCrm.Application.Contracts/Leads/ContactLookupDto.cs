using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.Leads;

public class ContactLookupDto : EntityDto<int>
{
	public string? Name { get; set; }
}

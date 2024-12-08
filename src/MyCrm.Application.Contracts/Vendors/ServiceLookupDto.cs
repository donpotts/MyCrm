using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.Vendors;

public class ServiceLookupDto : EntityDto<int>
{
	public string? Name { get; set; }
}

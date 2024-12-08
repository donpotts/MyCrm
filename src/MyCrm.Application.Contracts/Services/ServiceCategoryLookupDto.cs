using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.Services;

public class ServiceCategoryLookupDto : EntityDto<int>
{
	public string? Name { get; set; }
}

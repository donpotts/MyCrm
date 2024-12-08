using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.Vendors;

public class ProductLookupDto : EntityDto<int>
{
	public string? Name { get; set; }
}

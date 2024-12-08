using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.Leads;

public class AddressLookupDto : EntityDto<int>
{
	public string? City { get; set; }
}

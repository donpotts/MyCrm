using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.Customers;

public class ContactLookupDto : EntityDto<int>
{
	public string? Name { get; set; }
}

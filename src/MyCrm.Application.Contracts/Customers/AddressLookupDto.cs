using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.Customers;

public class AddressLookupDto : EntityDto<int>
{
	public string? City { get; set; }
}

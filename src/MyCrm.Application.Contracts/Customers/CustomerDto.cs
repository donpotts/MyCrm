using System;
using Volo.Abp.Application.Dtos;
using MyCrm.Addresses;
using MyCrm.Contacts;

namespace MyCrm.Customers;

public class CustomerDto : AuditedEntityDto<int>
{
    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Industry { get; set; }

    public string? Logo { get; set; }

    public string? Notes { get; set; }

    public AddressDto? Address { get; set; }

    public ContactDto? Contact { get; set; }
}

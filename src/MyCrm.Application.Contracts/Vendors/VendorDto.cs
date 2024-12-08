using System;
using Volo.Abp.Application.Dtos;
using MyCrm.Addresses;
using MyCrm.Services;
using MyCrm.Products;

namespace MyCrm.Vendors;

public class VendorDto : AuditedEntityDto<int>
{
    public string? Name { get; set; }

    public string? ContactName { get; set; }

    public int Phone { get; set; }

    public string? Email { get; set; }

    public string? Logo { get; set; }

    public string? Notes { get; set; }

    public AddressDto? Address { get; set; }

    public ServiceDto? Service { get; set; }

    public ProductDto? Product { get; set; }
}

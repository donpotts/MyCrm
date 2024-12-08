using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.Sales;

public class SaleDto : AuditedEntityDto<int>
{
    public string? ProductId { get; set; }

    public string? ServiceId { get; set; }

    public int CustomerId { get; set; }

    public int Quantity { get; set; }

    public double TotalAmount { get; set; }

    public DateTime SaleDate { get; set; }

    public string? ReceiptPhoto { get; set; }

    public string? Notes { get; set; }
}

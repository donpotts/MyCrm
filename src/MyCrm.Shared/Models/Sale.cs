
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class Sale
{
    [Key]
    public long Id { get; set; }
    
    public string? ProductId { get; set; }
    
    public string? ServiceId { get; set; }
    
    public int CustomerId { get; set; }
    
    public int Quantity { get; set; }
    
    public double TotalAmount { get; set; }
    
    public DateTime SaleDate { get; set; }
    
    public string? ReceiptPhoto { get; set; }
    
    public string? Notes { get; set; }
    
}

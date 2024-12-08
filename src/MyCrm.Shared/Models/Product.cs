
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class Product
{
    [Key]
    public long Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public double Price { get; set; }
    
    public int StockQuantity { get; set; }
    
    public string? Photo { get; set; }
    
    public string? Notes { get; set; }
    
    [DataMember]
    public long? ProductCategoryId { get; set; }
    
    [DataMember]
    public ProductCategory? ProductCategory { get; set; }

}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class ProductCategory
{
    [Key]
    public long Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Icon { get; set; }
    
    public string? TaxRate { get; set; }
    
    public string? Notes { get; set; }

}

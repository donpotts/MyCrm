
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class Service
{
    [Key]
    public long Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Recurring { get; set; }
    
    public string? Icon { get; set; }
    
    public string? Notes { get; set; }
    
    [DataMember]
    public long? ServiceCategoryId { get; set; }
    
    [DataMember]
    public ServiceCategory? ServiceCategory { get; set; }

}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class Vendor
{
    [Key]
    public long Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? ContactName { get; set; }
    
    public int Phone { get; set; }
    
    public string? Email { get; set; }
    
    public string? Logo { get; set; }
    
    public string? Notes { get; set; }
    
    [DataMember]
    public long? AddressId { get; set; }
    
    [DataMember]
    public Address? Address { get; set; }
    [DataMember]
    public long? ServiceId { get; set; }
    
    [DataMember]
    public Service? Service { get; set; }
    [DataMember]
    public long? ProductId { get; set; }
    
    [DataMember]
    public Product? Product { get; set; }

}

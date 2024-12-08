
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class Customer
{
    [Key]
    public long Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Type { get; set; }
    
    public string? Industry { get; set; }
    
    public string? Logo { get; set; }
    
    public string? Notes { get; set; }
    
    [DataMember]
    public long? AddressId { get; set; }
    
    [DataMember]
    public Address? Address { get; set; }
    [DataMember]
    public long? ContactId { get; set; }
    
    [DataMember]
    public Contact? Contact { get; set; }

}

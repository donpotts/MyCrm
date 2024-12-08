
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class Address
{
    [Key]
    public long Id { get; set; }
    
    public string? Street { get; set; }
    
    public string? City { get; set; }
    
    public string? State { get; set; }
    
    public int ZipCode { get; set; }
    
    public string? Country { get; set; }
    
    public string? Photo { get; set; }
    
    public string? Notes { get; set; }
 }

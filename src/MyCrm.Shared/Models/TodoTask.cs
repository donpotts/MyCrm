
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class TodoTask
{
    [Key]
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int AssignedTo { get; set; }

    public string? Status { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime ModifiedDateTime { get; set; }

    public int UserId { get; set; }

    public DateTime FollowupDate { get; set; }

    public string? Icon { get; set; }

    public string? Notes { get; set; }
   

}

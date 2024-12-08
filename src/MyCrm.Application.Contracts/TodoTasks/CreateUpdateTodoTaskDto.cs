using System;

namespace MyCrm.TodoTasks;

public class CreateUpdateTodoTaskDto
{
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

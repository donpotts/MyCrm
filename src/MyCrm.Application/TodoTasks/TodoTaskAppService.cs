using System;
using MyCrm.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.TodoTasks;

public class TodoTaskAppService :
    CrudAppService<
        TodoTask, //The TodoTask entity
        TodoTaskDto, //Used to show todo tasks
        int, //Primary key of the todo task entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateTodoTaskDto>, //Used to create/update a todo task
    ITodoTaskAppService //implement the ITodoTaskAppService
{
    public TodoTaskAppService(IRepository<TodoTask, int> repository)
        : base(repository)
    {
        GetPolicyName = MyCrmPermissions.TodoTasks.Default;
        GetListPolicyName = MyCrmPermissions.TodoTasks.Default;
        CreatePolicyName = MyCrmPermissions.TodoTasks.Create;
        UpdatePolicyName = MyCrmPermissions.TodoTasks.Edit;
        DeletePolicyName = MyCrmPermissions.TodoTasks.Delete;
    }
}

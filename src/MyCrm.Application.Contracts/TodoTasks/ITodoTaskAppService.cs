using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.TodoTasks;

public interface ITodoTaskAppService :
    ICrudAppService< //Defines CRUD methods
        TodoTaskDto, //Used to show todo tasks
        int, //Primary key of the todo task entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateTodoTaskDto> //Used to create/update a todo task
{
}

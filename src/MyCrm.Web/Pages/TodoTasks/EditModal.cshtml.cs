using System;
using System.Threading.Tasks;
using MyCrm.TodoTasks;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.TodoTasks;

public class EditModalModel : MyCrmPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public CreateUpdateTodoTaskDto TodoTask { get; set; } = null!;

    private readonly ITodoTaskAppService _todoTaskAppService;

    public EditModalModel(ITodoTaskAppService todoTaskAppService)
    {
        _todoTaskAppService = todoTaskAppService;
    }

    public async Task OnGetAsync()
    {
        var todoTaskDto = await _todoTaskAppService.GetAsync(Id);
        TodoTask = ObjectMapper.Map<TodoTaskDto, CreateUpdateTodoTaskDto>(todoTaskDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _todoTaskAppService.UpdateAsync(Id, TodoTask);
        return NoContent();
    }
}

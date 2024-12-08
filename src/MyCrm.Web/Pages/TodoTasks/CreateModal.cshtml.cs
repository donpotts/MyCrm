using System.Threading.Tasks;
using MyCrm.TodoTasks;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.TodoTasks;

public class CreateModalModel : MyCrmPageModel
{
    [BindProperty]
    public CreateUpdateTodoTaskDto TodoTask { get; set; } = null!;

    private readonly ITodoTaskAppService _todoTaskAppService;

    public CreateModalModel(ITodoTaskAppService todoTaskAppService)
    {
        _todoTaskAppService = todoTaskAppService;
    }

    public void OnGet()
    {
        TodoTask = new CreateUpdateTodoTaskDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _todoTaskAppService.CreateAsync(TodoTask);
        return NoContent();
    }
}

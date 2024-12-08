using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyCrm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace MyCrm.Web.Pages.Services;

public class EditModalModel : MyCrmPageModel
{

    [BindProperty]
    public EditServiceViewModel Service { get; set; } = null!;

    public List<SelectListItem> ServiceCategories { get; set; } = null!;

    private readonly IServiceAppService _serviceAppService;

    public EditModalModel(IServiceAppService serviceAppService)
    {
        _serviceAppService = serviceAppService;
    }

    public async Task OnGetAsync(int id)
    {
        var serviceDto = await _serviceAppService.GetAsync(id);
        Service = ObjectMapper.Map<ServiceDto, EditServiceViewModel>(serviceDto);
        Service.ServiceCategoryId = serviceDto.ServiceCategory?.Id;

        var serviceCategoryLookup = await _serviceAppService.GetServiceCategoryLookupAsync();
        ServiceCategories = serviceCategoryLookup.Items
            .OrderBy(x => x.Name)
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        ServiceCategories.Insert(0, new SelectListItem(string.Empty, null));
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _serviceAppService.UpdateAsync(
            Service.Id,
            ObjectMapper.Map<EditServiceViewModel, CreateUpdateServiceDto>(Service)
        );

        return NoContent();
    }

    public class EditServiceViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Recurring { get; set; }

        public string? Icon { get; set; }

        public string? Notes { get; set; }

        [SelectItems(nameof(ServiceCategories))]
        [DisplayName("ServiceCategory")]
        public int? ServiceCategoryId { get; set; }
    }
}

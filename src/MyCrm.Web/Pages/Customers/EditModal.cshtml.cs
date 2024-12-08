using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyCrm.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace MyCrm.Web.Pages.Customers;

public class EditModalModel : MyCrmPageModel
{

    [BindProperty]
    public EditCustomerViewModel Customer { get; set; } = null!;

    public List<SelectListItem> Addresses { get; set; } = null!;
    public List<SelectListItem> Contacts { get; set; } = null!;

    private readonly ICustomerAppService _customerAppService;

    public EditModalModel(ICustomerAppService customerAppService)
    {
        _customerAppService = customerAppService;
    }

    public async Task OnGetAsync(int id)
    {
        var customerDto = await _customerAppService.GetAsync(id);
        Customer = ObjectMapper.Map<CustomerDto, EditCustomerViewModel>(customerDto);
        Customer.AddressId = customerDto.Address?.Id;
        Customer.ContactId = customerDto.Contact?.Id;

        var addressLookup = await _customerAppService.GetAddressLookupAsync();
        Addresses = addressLookup.Items
            .OrderBy(x => x.City)
            .Select(x => new SelectListItem(x.City, x.Id.ToString()))
            .ToList();
        Addresses.Insert(0, new SelectListItem(string.Empty, null));

        var contactLookup = await _customerAppService.GetContactLookupAsync();
        Contacts = contactLookup.Items
            .OrderBy(x => x.Name)
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        Contacts.Insert(0, new SelectListItem(string.Empty, null));
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _customerAppService.UpdateAsync(
            Customer.Id,
            ObjectMapper.Map<EditCustomerViewModel, CreateUpdateCustomerDto>(Customer)
        );

        return NoContent();
    }

    public class EditCustomerViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Type { get; set; }

        public string? Industry { get; set; }

        public string? Logo { get; set; }

        public string? Notes { get; set; }

        [SelectItems(nameof(Addresses))]
        [DisplayName("Address")]
        public int? AddressId { get; set; }

        [SelectItems(nameof(Contacts))]
        [DisplayName("Contact")]
        public int? ContactId { get; set; }
    }
}

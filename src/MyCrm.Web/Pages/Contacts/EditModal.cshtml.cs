using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyCrm.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace MyCrm.Web.Pages.Contacts;

public class EditModalModel : MyCrmPageModel
{

    [BindProperty]
    public EditContactViewModel Contact { get; set; } = null!;

    public List<SelectListItem> Addresses { get; set; } = null!;
    public List<SelectListItem> Rewards { get; set; } = null!;

    private readonly IContactAppService _contactAppService;

    public EditModalModel(IContactAppService contactAppService)
    {
        _contactAppService = contactAppService;
    }

    public async Task OnGetAsync(int id)
    {
        var contactDto = await _contactAppService.GetAsync(id);
        Contact = ObjectMapper.Map<ContactDto, EditContactViewModel>(contactDto);
        Contact.AddressId = contactDto.Address?.Id;
        Contact.RewardId = contactDto.Reward?.Id;

        var addressLookup = await _contactAppService.GetAddressLookupAsync();
        Addresses = addressLookup.Items
            .OrderBy(x => x.City)
            .Select(x => new SelectListItem(x.City, x.Id.ToString()))
            .ToList();
        Addresses.Insert(0, new SelectListItem(string.Empty, null));

        var rewardLookup = await _contactAppService.GetRewardLookupAsync();
        Rewards = rewardLookup.Items
            .OrderBy(x => x.Rewardpoints)
            .Select(x => new SelectListItem(x.Rewardpoints.ToString(), x.Id.ToString()))
            .ToList();
        Rewards.Insert(0, new SelectListItem(string.Empty, null));
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _contactAppService.UpdateAsync(
            Contact.Id,
            ObjectMapper.Map<EditContactViewModel, CreateUpdateContactDto>(Contact)
        );

        return NoContent();
    }

    public class EditContactViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public int Phone { get; set; }

        public string? Role { get; set; }

        public string? Photo { get; set; }

        public string? Notes { get; set; }

        [SelectItems(nameof(Addresses))]
        [DisplayName("Address")]
        public int? AddressId { get; set; }

        [SelectItems(nameof(Rewards))]
        [DisplayName("Reward")]
        public int? RewardId { get; set; }
    }
}

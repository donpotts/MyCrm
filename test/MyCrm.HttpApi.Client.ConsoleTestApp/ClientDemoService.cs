using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Account;
using MyCrm.Contacts;
using Volo.Abp.Application.Dtos;
using static System.Reflection.Metadata.BlobBuilder;

namespace MyCrm.HttpApi.Client.ConsoleTestApp;

public class ClientDemoService : ITransientDependency
{
    private readonly IProfileAppService _profileAppService;
    private readonly IIdentityUserAppService _identityUserAppService;
    private readonly IContactAppService _contactAppService;
    public ClientDemoService(IProfileAppService profileAppService, IIdentityUserAppService identityUserAppService, IContactAppService contactAppService)
    {
        _profileAppService = profileAppService;
        _identityUserAppService = identityUserAppService;
        _contactAppService = contactAppService;
    }

    public async Task RunAsync()
    {
        var profileDto = await _profileAppService.GetAsync();
        Console.WriteLine($"UserName : {profileDto.UserName}");
        Console.WriteLine($"Email    : {profileDto.Email}");
        Console.WriteLine($"Name     : {profileDto.Name}");
        Console.WriteLine($"Surname  : {profileDto.Surname}");
        Console.WriteLine();

        var resultDto = await _identityUserAppService.GetListAsync(new GetIdentityUsersInput());
        Console.WriteLine($"Total users: {resultDto.TotalCount}");
        foreach (var identityUserDto in resultDto.Items)
        {
            Console.WriteLine($"- [{identityUserDto.Id}] {identityUserDto.Name}");
        }

        var contactListInput = new PagedAndSortedResultRequestDto();
        var contacts = await _contactAppService.GetListAsync(contactListInput);
        foreach (var contact in contacts.Items)
        {
            Console.WriteLine($"[Contacts Id: {contact.Id}] , Name={contact.Name} , Email={contact.Email} , Reward={contact.Reward} , Role={contact.Role} , Notes={contact.Notes} , City={contact.Address.City} , State={contact.Address.State} , Zipcode={contact.Address.ZipCode} , Country={contact.Address.Country}");
        }

        var contact1 = await _contactAppService.GetAsync(1);
        Console.WriteLine($"[Contacts Id: {contact1.Id}] , Name={contact1.Name} , Email={contact1.Email} , Reward={contact1.Reward} , Role={contact1.Role} , Notes={contact1.Notes} , City={contact1.Address.City} , State={contact1.Address.State} , Zipcode={contact1.Address.ZipCode} , Country={contact1.Address.Country}");
    }
}

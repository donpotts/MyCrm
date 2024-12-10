using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Account;
using MyCrm.Contacts;
using Volo.Abp.Application.Dtos;
using static System.Reflection.Metadata.BlobBuilder;
using MyCrm.Customers;
using MyCrm.Addresses;
using MyCrm.Leads;
using MyCrm.Opportunities;
using MyCrm.ProductCategories;
using MyCrm.Products;
using MyCrm.Rewards;
using MyCrm.Sales;
using MyCrm.ServiceCategories;
using MyCrm.Services;
using MyCrm.SupportCases;
using MyCrm.TodoTasks;
using MyCrm.Vendors;

namespace MyCrm.HttpApi.Client.ConsoleTestApp;

public class ClientDemoService : ITransientDependency
{
    private readonly IProfileAppService _profileAppService;
    private readonly IIdentityUserAppService _identityUserAppService;
    private readonly IAddressAppService _addressAppService;
    private readonly IContactAppService _contactAppService;
    private readonly ICustomerAppService _customerAppService;
    private readonly ILeadAppService _leadAppService;
    private readonly IOpportunityAppService _opportunityAppService;
    private readonly IProductCategoryAppService _productCategoryAppService;
    private readonly IProductAppService _productAppService;
    private readonly IRewardAppService _rewardAppService;
    private readonly ISaleAppService _saleAppService;
    private readonly IServiceCategoryAppService _serviceCategoryAppService;
    private readonly IServiceAppService _serviceAppService;
    private readonly ISupportCaseAppService _supportCaseAppService;
    private readonly ITodoTaskAppService _todoTaskAppService;
    private readonly IVendorAppService _vendorAppService;
    public ClientDemoService(IProfileAppService profileAppService, IIdentityUserAppService identityUserAppService, IAddressAppService addressAppService, 
        IContactAppService contactAppService,
        ICustomerAppService customerAppService,
        ILeadAppService leadAppService,
        IOpportunityAppService opportunityAppService,
        IProductCategoryAppService productCategoryAppService,
        IProductAppService productAppService,
        IRewardAppService rewardAppService,
        ISaleAppService saleAppService,
        IServiceCategoryAppService serviceCategoryAppService,
        IServiceAppService serviceAppService,
        ISupportCaseAppService supportCaseAppService,
        ITodoTaskAppService todoTaskAppService,
        IVendorAppService vendorAppService
        )
    {
        _profileAppService = profileAppService;
        _identityUserAppService = identityUserAppService;
        _contactAppService = contactAppService;
        _addressAppService = addressAppService;
        _contactAppService = contactAppService;
        _customerAppService = customerAppService;
        _leadAppService = leadAppService;
        _opportunityAppService = opportunityAppService;
        _productCategoryAppService = productCategoryAppService;
        _productAppService = productAppService;
        _rewardAppService = rewardAppService;
        _saleAppService = saleAppService;
        _serviceCategoryAppService = serviceCategoryAppService;
        _serviceAppService = serviceAppService;
        _supportCaseAppService = supportCaseAppService;
        _todoTaskAppService = todoTaskAppService;
        _vendorAppService = vendorAppService;
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
        var customerListInput = new PagedAndSortedResultRequestDto();
        var customers = await _customerAppService.GetListAsync(customerListInput);
        foreach (var customer in customers.Items)
        {
            Console.WriteLine($"[Customer Id: {customer.Id}] , Name={customer.Name} , City={customer.Address.City}");
        }

        var leadListInput = new PagedAndSortedResultRequestDto();
        var leads = await _leadAppService.GetListAsync(leadListInput);
        foreach (var lead in leads.Items)
        {
            Console.WriteLine($"[Lead Id: {lead.Id}] , Name={lead.Contact.Name} , City={lead.Address.City}");
        }

        var opportunityListInput = new PagedAndSortedResultRequestDto();
        var opportunities = await _opportunityAppService.GetListAsync(opportunityListInput);
        foreach (var opportunity in opportunities.Items)
        {
            Console.WriteLine($"[Opportunity Id: {opportunity.Id}] , Name={opportunity.Notes} , City={opportunity.Stage}");
        }

        var productCategoryListInput = new PagedAndSortedResultRequestDto();
        var productCategories = await _productCategoryAppService.GetListAsync(productCategoryListInput);
        foreach (var productCategory in productCategories.Items)
        {
            Console.WriteLine($"[Product Category Id: {productCategory.Id}] , Name={productCategory.Name}");
        }

        var productListInput = new PagedAndSortedResultRequestDto();
        var products = await _productAppService.GetListAsync(productListInput);
        foreach (var product in products.Items)
        {
            Console.WriteLine($"[Product Id: {product.Id}] , Name={product.Name}");
        }

        var rewardListInput = new PagedAndSortedResultRequestDto();
        var rewards = await _rewardAppService.GetListAsync(rewardListInput);
        foreach (var reward in rewards.Items)
        {
            Console.WriteLine($"[Reward Id: {reward.Id}] , Name={reward.Rewardpoints}");
        }

        var saleListInput = new PagedAndSortedResultRequestDto();
        var sales = await _saleAppService.GetListAsync(saleListInput);
        foreach (var sale in sales.Items)
        {
            Console.WriteLine($"[Sale Id: {sale.Id}] , Name={sale.TotalAmount}");
        }

        var serviceCategoryListInput = new PagedAndSortedResultRequestDto();
        var serviceCategories = await _serviceCategoryAppService.GetListAsync(serviceCategoryListInput);
        foreach (var serviceCategory in serviceCategories.Items)
        {
            Console.WriteLine($"[Service Category Id: {serviceCategory.Id}] , Name={serviceCategory.Name}");
        }

        var serviceListInput = new PagedAndSortedResultRequestDto();
        var services = await _serviceAppService.GetListAsync(serviceListInput);
        foreach (var service in services.Items)
        {
            Console.WriteLine($"[Service Id: {service.Id}] , Name={service.Name}");
        }

        var supportCaseListInput = new PagedAndSortedResultRequestDto();
        var supportCases = await _supportCaseAppService.GetListAsync(supportCaseListInput);
        foreach (var supportCase in supportCases.Items)
        {
            Console.WriteLine($"[Support Case Id: {supportCase.Id}] , Name={supportCase.Description}");
        }

        var todoTaskListInput = new PagedAndSortedResultRequestDto();
        var todoTasks = await _todoTaskAppService.GetListAsync(todoTaskListInput);
        foreach (var todoTask in todoTasks.Items)
        {
            Console.WriteLine($"[Todo Task Id: {todoTask.Id}] , Name={todoTask.Name}");
        }

        var vendorListInput = new PagedAndSortedResultRequestDto();
        var vendors = await _vendorAppService.GetListAsync(vendorListInput);
        foreach (var vendor in vendors.Items)
        {
            Console.WriteLine($"[Vendor Id: {vendor.Id}] , Name={vendor.Name} , City={vendor.Address.City}");
        }
    }
}
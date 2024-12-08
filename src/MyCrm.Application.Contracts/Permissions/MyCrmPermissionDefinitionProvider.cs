using MyCrm.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MyCrm.Permissions;

public class MyCrmPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myCrmGroup = context.AddGroup(MyCrmPermissions.GroupName, L("Permission:MyCrm"));

        var customersPermission = myCrmGroup.AddPermission(MyCrmPermissions.Customers.Default, L("Permission:Customers"));
        customersPermission.AddChild(MyCrmPermissions.Customers.Create, L("Permission:Customers.Create"));
        customersPermission.AddChild(MyCrmPermissions.Customers.Edit, L("Permission:Customers.Edit"));
        customersPermission.AddChild(MyCrmPermissions.Customers.Delete, L("Permission:Customers.Delete"));
        var addressesPermission = myCrmGroup.AddPermission(MyCrmPermissions.Addresses.Default, L("Permission:Addresses"));
        addressesPermission.AddChild(MyCrmPermissions.Addresses.Create, L("Permission:Addresses.Create"));
        addressesPermission.AddChild(MyCrmPermissions.Addresses.Edit, L("Permission:Addresses.Edit"));
        addressesPermission.AddChild(MyCrmPermissions.Addresses.Delete, L("Permission:Addresses.Delete"));
        var productCategoriesPermission = myCrmGroup.AddPermission(MyCrmPermissions.ProductCategories.Default, L("Permission:ProductCategories"));
        productCategoriesPermission.AddChild(MyCrmPermissions.ProductCategories.Create, L("Permission:ProductCategories.Create"));
        productCategoriesPermission.AddChild(MyCrmPermissions.ProductCategories.Edit, L("Permission:ProductCategories.Edit"));
        productCategoriesPermission.AddChild(MyCrmPermissions.ProductCategories.Delete, L("Permission:ProductCategories.Delete"));
        var serviceCategoriesPermission = myCrmGroup.AddPermission(MyCrmPermissions.ServiceCategories.Default, L("Permission:ServiceCategories"));
        serviceCategoriesPermission.AddChild(MyCrmPermissions.ServiceCategories.Create, L("Permission:ServiceCategories.Create"));
        serviceCategoriesPermission.AddChild(MyCrmPermissions.ServiceCategories.Edit, L("Permission:ServiceCategories.Edit"));
        serviceCategoriesPermission.AddChild(MyCrmPermissions.ServiceCategories.Delete, L("Permission:ServiceCategories.Delete"));
        var contactsPermission = myCrmGroup.AddPermission(MyCrmPermissions.Contacts.Default, L("Permission:Contacts"));
        contactsPermission.AddChild(MyCrmPermissions.Contacts.Create, L("Permission:Contacts.Create"));
        contactsPermission.AddChild(MyCrmPermissions.Contacts.Edit, L("Permission:Contacts.Edit"));
        contactsPermission.AddChild(MyCrmPermissions.Contacts.Delete, L("Permission:Contacts.Delete"));
        var opportunitiesPermission = myCrmGroup.AddPermission(MyCrmPermissions.Opportunities.Default, L("Permission:Opportunities"));
        opportunitiesPermission.AddChild(MyCrmPermissions.Opportunities.Create, L("Permission:Opportunities.Create"));
        opportunitiesPermission.AddChild(MyCrmPermissions.Opportunities.Edit, L("Permission:Opportunities.Edit"));
        opportunitiesPermission.AddChild(MyCrmPermissions.Opportunities.Delete, L("Permission:Opportunities.Delete"));
        var leadsPermission = myCrmGroup.AddPermission(MyCrmPermissions.Leads.Default, L("Permission:Leads"));
        leadsPermission.AddChild(MyCrmPermissions.Leads.Create, L("Permission:Leads.Create"));
        leadsPermission.AddChild(MyCrmPermissions.Leads.Edit, L("Permission:Leads.Edit"));
        leadsPermission.AddChild(MyCrmPermissions.Leads.Delete, L("Permission:Leads.Delete"));
        var productsPermission = myCrmGroup.AddPermission(MyCrmPermissions.Products.Default, L("Permission:Products"));
        productsPermission.AddChild(MyCrmPermissions.Products.Create, L("Permission:Products.Create"));
        productsPermission.AddChild(MyCrmPermissions.Products.Edit, L("Permission:Products.Edit"));
        productsPermission.AddChild(MyCrmPermissions.Products.Delete, L("Permission:Products.Delete"));
        var servicesPermission = myCrmGroup.AddPermission(MyCrmPermissions.Services.Default, L("Permission:Services"));
        servicesPermission.AddChild(MyCrmPermissions.Services.Create, L("Permission:Services.Create"));
        servicesPermission.AddChild(MyCrmPermissions.Services.Edit, L("Permission:Services.Edit"));
        servicesPermission.AddChild(MyCrmPermissions.Services.Delete, L("Permission:Services.Delete"));
        var salesPermission = myCrmGroup.AddPermission(MyCrmPermissions.Sales.Default, L("Permission:Sales"));
        salesPermission.AddChild(MyCrmPermissions.Sales.Create, L("Permission:Sales.Create"));
        salesPermission.AddChild(MyCrmPermissions.Sales.Edit, L("Permission:Sales.Edit"));
        salesPermission.AddChild(MyCrmPermissions.Sales.Delete, L("Permission:Sales.Delete"));
        var vendorsPermission = myCrmGroup.AddPermission(MyCrmPermissions.Vendors.Default, L("Permission:Vendors"));
        vendorsPermission.AddChild(MyCrmPermissions.Vendors.Create, L("Permission:Vendors.Create"));
        vendorsPermission.AddChild(MyCrmPermissions.Vendors.Edit, L("Permission:Vendors.Edit"));
        vendorsPermission.AddChild(MyCrmPermissions.Vendors.Delete, L("Permission:Vendors.Delete"));
        var supportCasesPermission = myCrmGroup.AddPermission(MyCrmPermissions.SupportCases.Default, L("Permission:SupportCases"));
        supportCasesPermission.AddChild(MyCrmPermissions.SupportCases.Create, L("Permission:SupportCases.Create"));
        supportCasesPermission.AddChild(MyCrmPermissions.SupportCases.Edit, L("Permission:SupportCases.Edit"));
        supportCasesPermission.AddChild(MyCrmPermissions.SupportCases.Delete, L("Permission:SupportCases.Delete"));
        var todoTasksPermission = myCrmGroup.AddPermission(MyCrmPermissions.TodoTasks.Default, L("Permission:TodoTasks"));
        todoTasksPermission.AddChild(MyCrmPermissions.TodoTasks.Create, L("Permission:TodoTasks.Create"));
        todoTasksPermission.AddChild(MyCrmPermissions.TodoTasks.Edit, L("Permission:TodoTasks.Edit"));
        todoTasksPermission.AddChild(MyCrmPermissions.TodoTasks.Delete, L("Permission:TodoTasks.Delete"));
        var rewardsPermission = myCrmGroup.AddPermission(MyCrmPermissions.Rewards.Default, L("Permission:Rewards"));
        rewardsPermission.AddChild(MyCrmPermissions.Rewards.Create, L("Permission:Rewards.Create"));
        rewardsPermission.AddChild(MyCrmPermissions.Rewards.Edit, L("Permission:Rewards.Edit"));
        rewardsPermission.AddChild(MyCrmPermissions.Rewards.Delete, L("Permission:Rewards.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MyCrmResource>(name);
    }
}

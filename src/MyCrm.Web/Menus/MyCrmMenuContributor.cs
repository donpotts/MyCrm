using System.Threading.Tasks;
using MyCrm.Localization;
using MyCrm.MultiTenancy;
using MyCrm.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace MyCrm.Web.Menus;

public class MyCrmMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<MyCrmResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                MyCrmMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "MyCrm",
                l["Menu:MyCrm"],
                icon: "fa fa-database")
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.Customers",
                    l["Menu:Customers"],
                    url: "/Customers"
                ).RequirePermissions(MyCrmPermissions.Customers.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.Addresses",
                    l["Menu:Addresses"],
                    url: "/Addresses"
                ).RequirePermissions(MyCrmPermissions.Addresses.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.ProductCategories",
                    l["Menu:ProductCategories"],
                    url: "/ProductCategories"
                ).RequirePermissions(MyCrmPermissions.ProductCategories.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.ServiceCategories",
                    l["Menu:ServiceCategories"],
                    url: "/ServiceCategories"
                ).RequirePermissions(MyCrmPermissions.ServiceCategories.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.Contacts",
                    l["Menu:Contacts"],
                    url: "/Contacts"
                ).RequirePermissions(MyCrmPermissions.Contacts.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.Opportunities",
                    l["Menu:Opportunities"],
                    url: "/Opportunities"
                ).RequirePermissions(MyCrmPermissions.Opportunities.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.Leads",
                    l["Menu:Leads"],
                    url: "/Leads"
                ).RequirePermissions(MyCrmPermissions.Leads.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.Products",
                    l["Menu:Products"],
                    url: "/Products"
                ).RequirePermissions(MyCrmPermissions.Products.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.Services",
                    l["Menu:Services"],
                    url: "/Services"
                ).RequirePermissions(MyCrmPermissions.Services.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.Sales",
                    l["Menu:Sales"],
                    url: "/Sales"
                ).RequirePermissions(MyCrmPermissions.Sales.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.Vendors",
                    l["Menu:Vendors"],
                    url: "/Vendors"
                ).RequirePermissions(MyCrmPermissions.Vendors.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.SupportCases",
                    l["Menu:SupportCases"],
                    url: "/SupportCases"
                ).RequirePermissions(MyCrmPermissions.SupportCases.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.TodoTasks",
                    l["Menu:TodoTasks"],
                    url: "/TodoTasks"
                ).RequirePermissions(MyCrmPermissions.TodoTasks.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "MyCrm.Rewards",
                    l["Menu:Rewards"],
                    url: "/Rewards"
                ).RequirePermissions(MyCrmPermissions.Rewards.Default) // Check the permission!
            )
        );

        return Task.CompletedTask;
    }
}

using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCrm.EntityFrameworkCore;
using MyCrm.Localization;
using MyCrm.MultiTenancy;
using MyCrm.Permissions;
using MyCrm.Web.Menus;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Studio;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Identity.Web;
using Volo.Abp.FeatureManagement;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp.TenantManagement.Web;
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Identity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.OpenIddict;
using Volo.Abp.Security.Claims;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Studio.Client.AspNetCore;

namespace MyCrm.Web;

[DependsOn(
    typeof(MyCrmHttpApiModule),
    typeof(MyCrmApplicationModule),
    typeof(MyCrmEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpStudioClientAspNetCoreModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpFeatureManagementWebModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public class MyCrmWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.Trim().RemovePostFix("/"))
                            .ToArray() ?? []
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(MyCrmResource),
                typeof(MyCrmDomainModule).Assembly,
                typeof(MyCrmDomainSharedModule).Assembly,
                typeof(MyCrmApplicationModule).Assembly,
                typeof(MyCrmApplicationContractsModule).Assembly,
                typeof(MyCrmWebModule).Assembly
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("MyCrm");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

        if (!hostingEnvironment.IsDevelopment())
        {
            PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
            {
                options.AddDevelopmentEncryptionAndSigningCertificate = false;
            });

            PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
            {
                serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", configuration["AuthServer:CertificatePassPhrase"]!);
                serverBuilder.SetIssuer(new Uri(configuration["AuthServer:Authority"]!));
            });
        }
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);

            options.Conventions.AuthorizePage("/ServiceCategories/Index", MyCrmPermissions.ServiceCategories.Default);
            options.Conventions.AuthorizePage("/ServiceCategories/CreateModal", MyCrmPermissions.ServiceCategories.Create);
            options.Conventions.AuthorizePage("/ServiceCategories/EditModal", MyCrmPermissions.ServiceCategories.Edit);

            options.Conventions.AuthorizePage("/Contacts/Index", MyCrmPermissions.Contacts.Default);
            options.Conventions.AuthorizePage("/Contacts/CreateModal", MyCrmPermissions.Contacts.Create);
            options.Conventions.AuthorizePage("/Contacts/EditModal", MyCrmPermissions.Contacts.Edit);

            options.Conventions.AuthorizePage("/Opportunities/Index", MyCrmPermissions.Opportunities.Default);
            options.Conventions.AuthorizePage("/Opportunities/CreateModal", MyCrmPermissions.Opportunities.Create);
            options.Conventions.AuthorizePage("/Opportunities/EditModal", MyCrmPermissions.Opportunities.Edit);

            options.Conventions.AuthorizePage("/Leads/Index", MyCrmPermissions.Leads.Default);
            options.Conventions.AuthorizePage("/Leads/CreateModal", MyCrmPermissions.Leads.Create);
            options.Conventions.AuthorizePage("/Leads/EditModal", MyCrmPermissions.Leads.Edit);

            options.Conventions.AuthorizePage("/Products/Index", MyCrmPermissions.Products.Default);
            options.Conventions.AuthorizePage("/Products/CreateModal", MyCrmPermissions.Products.Create);
            options.Conventions.AuthorizePage("/Products/EditModal", MyCrmPermissions.Products.Edit);

            options.Conventions.AuthorizePage("/Services/Index", MyCrmPermissions.Services.Default);
            options.Conventions.AuthorizePage("/Services/CreateModal", MyCrmPermissions.Services.Create);
            options.Conventions.AuthorizePage("/Services/EditModal", MyCrmPermissions.Services.Edit);

            options.Conventions.AuthorizePage("/Sales/Index", MyCrmPermissions.Sales.Default);
            options.Conventions.AuthorizePage("/Sales/CreateModal", MyCrmPermissions.Sales.Create);
            options.Conventions.AuthorizePage("/Sales/EditModal", MyCrmPermissions.Sales.Edit);

            options.Conventions.AuthorizePage("/Vendors/Index", MyCrmPermissions.Vendors.Default);
            options.Conventions.AuthorizePage("/Vendors/CreateModal", MyCrmPermissions.Vendors.Create);
            options.Conventions.AuthorizePage("/Vendors/EditModal", MyCrmPermissions.Vendors.Edit);

            options.Conventions.AuthorizePage("/SupportCases/Index", MyCrmPermissions.SupportCases.Default);
            options.Conventions.AuthorizePage("/SupportCases/CreateModal", MyCrmPermissions.SupportCases.Create);
            options.Conventions.AuthorizePage("/SupportCases/EditModal", MyCrmPermissions.SupportCases.Edit);

            options.Conventions.AuthorizePage("/TodoTasks/Index", MyCrmPermissions.TodoTasks.Default);
            options.Conventions.AuthorizePage("/TodoTasks/CreateModal", MyCrmPermissions.TodoTasks.Create);
            options.Conventions.AuthorizePage("/TodoTasks/EditModal", MyCrmPermissions.TodoTasks.Edit);

            options.Conventions.AuthorizePage("/Rewards/Index", MyCrmPermissions.Rewards.Default);
            options.Conventions.AuthorizePage("/Rewards/CreateModal", MyCrmPermissions.Rewards.Create);
            options.Conventions.AuthorizePage("/Rewards/EditModal", MyCrmPermissions.Rewards.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);

            options.Conventions.AuthorizePage("/ServiceCategories/Index", MyCrmPermissions.ServiceCategories.Default);
            options.Conventions.AuthorizePage("/ServiceCategories/CreateModal", MyCrmPermissions.ServiceCategories.Create);
            options.Conventions.AuthorizePage("/ServiceCategories/EditModal", MyCrmPermissions.ServiceCategories.Edit);

            options.Conventions.AuthorizePage("/Contacts/Index", MyCrmPermissions.Contacts.Default);
            options.Conventions.AuthorizePage("/Contacts/CreateModal", MyCrmPermissions.Contacts.Create);
            options.Conventions.AuthorizePage("/Contacts/EditModal", MyCrmPermissions.Contacts.Edit);

            options.Conventions.AuthorizePage("/Opportunities/Index", MyCrmPermissions.Opportunities.Default);
            options.Conventions.AuthorizePage("/Opportunities/CreateModal", MyCrmPermissions.Opportunities.Create);
            options.Conventions.AuthorizePage("/Opportunities/EditModal", MyCrmPermissions.Opportunities.Edit);

            options.Conventions.AuthorizePage("/Leads/Index", MyCrmPermissions.Leads.Default);
            options.Conventions.AuthorizePage("/Leads/CreateModal", MyCrmPermissions.Leads.Create);
            options.Conventions.AuthorizePage("/Leads/EditModal", MyCrmPermissions.Leads.Edit);

            options.Conventions.AuthorizePage("/Products/Index", MyCrmPermissions.Products.Default);
            options.Conventions.AuthorizePage("/Products/CreateModal", MyCrmPermissions.Products.Create);
            options.Conventions.AuthorizePage("/Products/EditModal", MyCrmPermissions.Products.Edit);

            options.Conventions.AuthorizePage("/Services/Index", MyCrmPermissions.Services.Default);
            options.Conventions.AuthorizePage("/Services/CreateModal", MyCrmPermissions.Services.Create);
            options.Conventions.AuthorizePage("/Services/EditModal", MyCrmPermissions.Services.Edit);

            options.Conventions.AuthorizePage("/Sales/Index", MyCrmPermissions.Sales.Default);
            options.Conventions.AuthorizePage("/Sales/CreateModal", MyCrmPermissions.Sales.Create);
            options.Conventions.AuthorizePage("/Sales/EditModal", MyCrmPermissions.Sales.Edit);

            options.Conventions.AuthorizePage("/Vendors/Index", MyCrmPermissions.Vendors.Default);
            options.Conventions.AuthorizePage("/Vendors/CreateModal", MyCrmPermissions.Vendors.Create);
            options.Conventions.AuthorizePage("/Vendors/EditModal", MyCrmPermissions.Vendors.Edit);

            options.Conventions.AuthorizePage("/SupportCases/Index", MyCrmPermissions.SupportCases.Default);
            options.Conventions.AuthorizePage("/SupportCases/CreateModal", MyCrmPermissions.SupportCases.Create);
            options.Conventions.AuthorizePage("/SupportCases/EditModal", MyCrmPermissions.SupportCases.Edit);

            options.Conventions.AuthorizePage("/TodoTasks/Index", MyCrmPermissions.TodoTasks.Default);
            options.Conventions.AuthorizePage("/TodoTasks/CreateModal", MyCrmPermissions.TodoTasks.Create);
            options.Conventions.AuthorizePage("/TodoTasks/EditModal", MyCrmPermissions.TodoTasks.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);

            options.Conventions.AuthorizePage("/ServiceCategories/Index", MyCrmPermissions.ServiceCategories.Default);
            options.Conventions.AuthorizePage("/ServiceCategories/CreateModal", MyCrmPermissions.ServiceCategories.Create);
            options.Conventions.AuthorizePage("/ServiceCategories/EditModal", MyCrmPermissions.ServiceCategories.Edit);

            options.Conventions.AuthorizePage("/Contacts/Index", MyCrmPermissions.Contacts.Default);
            options.Conventions.AuthorizePage("/Contacts/CreateModal", MyCrmPermissions.Contacts.Create);
            options.Conventions.AuthorizePage("/Contacts/EditModal", MyCrmPermissions.Contacts.Edit);

            options.Conventions.AuthorizePage("/Opportunities/Index", MyCrmPermissions.Opportunities.Default);
            options.Conventions.AuthorizePage("/Opportunities/CreateModal", MyCrmPermissions.Opportunities.Create);
            options.Conventions.AuthorizePage("/Opportunities/EditModal", MyCrmPermissions.Opportunities.Edit);

            options.Conventions.AuthorizePage("/Leads/Index", MyCrmPermissions.Leads.Default);
            options.Conventions.AuthorizePage("/Leads/CreateModal", MyCrmPermissions.Leads.Create);
            options.Conventions.AuthorizePage("/Leads/EditModal", MyCrmPermissions.Leads.Edit);

            options.Conventions.AuthorizePage("/Products/Index", MyCrmPermissions.Products.Default);
            options.Conventions.AuthorizePage("/Products/CreateModal", MyCrmPermissions.Products.Create);
            options.Conventions.AuthorizePage("/Products/EditModal", MyCrmPermissions.Products.Edit);

            options.Conventions.AuthorizePage("/Services/Index", MyCrmPermissions.Services.Default);
            options.Conventions.AuthorizePage("/Services/CreateModal", MyCrmPermissions.Services.Create);
            options.Conventions.AuthorizePage("/Services/EditModal", MyCrmPermissions.Services.Edit);

            options.Conventions.AuthorizePage("/Sales/Index", MyCrmPermissions.Sales.Default);
            options.Conventions.AuthorizePage("/Sales/CreateModal", MyCrmPermissions.Sales.Create);
            options.Conventions.AuthorizePage("/Sales/EditModal", MyCrmPermissions.Sales.Edit);

            options.Conventions.AuthorizePage("/Vendors/Index", MyCrmPermissions.Vendors.Default);
            options.Conventions.AuthorizePage("/Vendors/CreateModal", MyCrmPermissions.Vendors.Create);
            options.Conventions.AuthorizePage("/Vendors/EditModal", MyCrmPermissions.Vendors.Edit);

            options.Conventions.AuthorizePage("/SupportCases/Index", MyCrmPermissions.SupportCases.Default);
            options.Conventions.AuthorizePage("/SupportCases/CreateModal", MyCrmPermissions.SupportCases.Create);
            options.Conventions.AuthorizePage("/SupportCases/EditModal", MyCrmPermissions.SupportCases.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);

            options.Conventions.AuthorizePage("/ServiceCategories/Index", MyCrmPermissions.ServiceCategories.Default);
            options.Conventions.AuthorizePage("/ServiceCategories/CreateModal", MyCrmPermissions.ServiceCategories.Create);
            options.Conventions.AuthorizePage("/ServiceCategories/EditModal", MyCrmPermissions.ServiceCategories.Edit);

            options.Conventions.AuthorizePage("/Contacts/Index", MyCrmPermissions.Contacts.Default);
            options.Conventions.AuthorizePage("/Contacts/CreateModal", MyCrmPermissions.Contacts.Create);
            options.Conventions.AuthorizePage("/Contacts/EditModal", MyCrmPermissions.Contacts.Edit);

            options.Conventions.AuthorizePage("/Opportunities/Index", MyCrmPermissions.Opportunities.Default);
            options.Conventions.AuthorizePage("/Opportunities/CreateModal", MyCrmPermissions.Opportunities.Create);
            options.Conventions.AuthorizePage("/Opportunities/EditModal", MyCrmPermissions.Opportunities.Edit);

            options.Conventions.AuthorizePage("/Leads/Index", MyCrmPermissions.Leads.Default);
            options.Conventions.AuthorizePage("/Leads/CreateModal", MyCrmPermissions.Leads.Create);
            options.Conventions.AuthorizePage("/Leads/EditModal", MyCrmPermissions.Leads.Edit);

            options.Conventions.AuthorizePage("/Products/Index", MyCrmPermissions.Products.Default);
            options.Conventions.AuthorizePage("/Products/CreateModal", MyCrmPermissions.Products.Create);
            options.Conventions.AuthorizePage("/Products/EditModal", MyCrmPermissions.Products.Edit);

            options.Conventions.AuthorizePage("/Services/Index", MyCrmPermissions.Services.Default);
            options.Conventions.AuthorizePage("/Services/CreateModal", MyCrmPermissions.Services.Create);
            options.Conventions.AuthorizePage("/Services/EditModal", MyCrmPermissions.Services.Edit);

            options.Conventions.AuthorizePage("/Sales/Index", MyCrmPermissions.Sales.Default);
            options.Conventions.AuthorizePage("/Sales/CreateModal", MyCrmPermissions.Sales.Create);
            options.Conventions.AuthorizePage("/Sales/EditModal", MyCrmPermissions.Sales.Edit);

            options.Conventions.AuthorizePage("/Vendors/Index", MyCrmPermissions.Vendors.Default);
            options.Conventions.AuthorizePage("/Vendors/CreateModal", MyCrmPermissions.Vendors.Create);
            options.Conventions.AuthorizePage("/Vendors/EditModal", MyCrmPermissions.Vendors.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);

            options.Conventions.AuthorizePage("/ServiceCategories/Index", MyCrmPermissions.ServiceCategories.Default);
            options.Conventions.AuthorizePage("/ServiceCategories/CreateModal", MyCrmPermissions.ServiceCategories.Create);
            options.Conventions.AuthorizePage("/ServiceCategories/EditModal", MyCrmPermissions.ServiceCategories.Edit);

            options.Conventions.AuthorizePage("/Contacts/Index", MyCrmPermissions.Contacts.Default);
            options.Conventions.AuthorizePage("/Contacts/CreateModal", MyCrmPermissions.Contacts.Create);
            options.Conventions.AuthorizePage("/Contacts/EditModal", MyCrmPermissions.Contacts.Edit);

            options.Conventions.AuthorizePage("/Opportunities/Index", MyCrmPermissions.Opportunities.Default);
            options.Conventions.AuthorizePage("/Opportunities/CreateModal", MyCrmPermissions.Opportunities.Create);
            options.Conventions.AuthorizePage("/Opportunities/EditModal", MyCrmPermissions.Opportunities.Edit);

            options.Conventions.AuthorizePage("/Leads/Index", MyCrmPermissions.Leads.Default);
            options.Conventions.AuthorizePage("/Leads/CreateModal", MyCrmPermissions.Leads.Create);
            options.Conventions.AuthorizePage("/Leads/EditModal", MyCrmPermissions.Leads.Edit);

            options.Conventions.AuthorizePage("/Products/Index", MyCrmPermissions.Products.Default);
            options.Conventions.AuthorizePage("/Products/CreateModal", MyCrmPermissions.Products.Create);
            options.Conventions.AuthorizePage("/Products/EditModal", MyCrmPermissions.Products.Edit);

            options.Conventions.AuthorizePage("/Services/Index", MyCrmPermissions.Services.Default);
            options.Conventions.AuthorizePage("/Services/CreateModal", MyCrmPermissions.Services.Create);
            options.Conventions.AuthorizePage("/Services/EditModal", MyCrmPermissions.Services.Edit);

            options.Conventions.AuthorizePage("/Sales/Index", MyCrmPermissions.Sales.Default);
            options.Conventions.AuthorizePage("/Sales/CreateModal", MyCrmPermissions.Sales.Create);
            options.Conventions.AuthorizePage("/Sales/EditModal", MyCrmPermissions.Sales.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);

            options.Conventions.AuthorizePage("/ServiceCategories/Index", MyCrmPermissions.ServiceCategories.Default);
            options.Conventions.AuthorizePage("/ServiceCategories/CreateModal", MyCrmPermissions.ServiceCategories.Create);
            options.Conventions.AuthorizePage("/ServiceCategories/EditModal", MyCrmPermissions.ServiceCategories.Edit);

            options.Conventions.AuthorizePage("/Contacts/Index", MyCrmPermissions.Contacts.Default);
            options.Conventions.AuthorizePage("/Contacts/CreateModal", MyCrmPermissions.Contacts.Create);
            options.Conventions.AuthorizePage("/Contacts/EditModal", MyCrmPermissions.Contacts.Edit);

            options.Conventions.AuthorizePage("/Opportunities/Index", MyCrmPermissions.Opportunities.Default);
            options.Conventions.AuthorizePage("/Opportunities/CreateModal", MyCrmPermissions.Opportunities.Create);
            options.Conventions.AuthorizePage("/Opportunities/EditModal", MyCrmPermissions.Opportunities.Edit);

            options.Conventions.AuthorizePage("/Leads/Index", MyCrmPermissions.Leads.Default);
            options.Conventions.AuthorizePage("/Leads/CreateModal", MyCrmPermissions.Leads.Create);
            options.Conventions.AuthorizePage("/Leads/EditModal", MyCrmPermissions.Leads.Edit);

            options.Conventions.AuthorizePage("/Products/Index", MyCrmPermissions.Products.Default);
            options.Conventions.AuthorizePage("/Products/CreateModal", MyCrmPermissions.Products.Create);
            options.Conventions.AuthorizePage("/Products/EditModal", MyCrmPermissions.Products.Edit);

            options.Conventions.AuthorizePage("/Services/Index", MyCrmPermissions.Services.Default);
            options.Conventions.AuthorizePage("/Services/CreateModal", MyCrmPermissions.Services.Create);
            options.Conventions.AuthorizePage("/Services/EditModal", MyCrmPermissions.Services.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);

            options.Conventions.AuthorizePage("/ServiceCategories/Index", MyCrmPermissions.ServiceCategories.Default);
            options.Conventions.AuthorizePage("/ServiceCategories/CreateModal", MyCrmPermissions.ServiceCategories.Create);
            options.Conventions.AuthorizePage("/ServiceCategories/EditModal", MyCrmPermissions.ServiceCategories.Edit);

            options.Conventions.AuthorizePage("/Contacts/Index", MyCrmPermissions.Contacts.Default);
            options.Conventions.AuthorizePage("/Contacts/CreateModal", MyCrmPermissions.Contacts.Create);
            options.Conventions.AuthorizePage("/Contacts/EditModal", MyCrmPermissions.Contacts.Edit);

            options.Conventions.AuthorizePage("/Opportunities/Index", MyCrmPermissions.Opportunities.Default);
            options.Conventions.AuthorizePage("/Opportunities/CreateModal", MyCrmPermissions.Opportunities.Create);
            options.Conventions.AuthorizePage("/Opportunities/EditModal", MyCrmPermissions.Opportunities.Edit);

            options.Conventions.AuthorizePage("/Leads/Index", MyCrmPermissions.Leads.Default);
            options.Conventions.AuthorizePage("/Leads/CreateModal", MyCrmPermissions.Leads.Create);
            options.Conventions.AuthorizePage("/Leads/EditModal", MyCrmPermissions.Leads.Edit);

            options.Conventions.AuthorizePage("/Products/Index", MyCrmPermissions.Products.Default);
            options.Conventions.AuthorizePage("/Products/CreateModal", MyCrmPermissions.Products.Create);
            options.Conventions.AuthorizePage("/Products/EditModal", MyCrmPermissions.Products.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);

            options.Conventions.AuthorizePage("/ServiceCategories/Index", MyCrmPermissions.ServiceCategories.Default);
            options.Conventions.AuthorizePage("/ServiceCategories/CreateModal", MyCrmPermissions.ServiceCategories.Create);
            options.Conventions.AuthorizePage("/ServiceCategories/EditModal", MyCrmPermissions.ServiceCategories.Edit);

            options.Conventions.AuthorizePage("/Contacts/Index", MyCrmPermissions.Contacts.Default);
            options.Conventions.AuthorizePage("/Contacts/CreateModal", MyCrmPermissions.Contacts.Create);
            options.Conventions.AuthorizePage("/Contacts/EditModal", MyCrmPermissions.Contacts.Edit);

            options.Conventions.AuthorizePage("/Opportunities/Index", MyCrmPermissions.Opportunities.Default);
            options.Conventions.AuthorizePage("/Opportunities/CreateModal", MyCrmPermissions.Opportunities.Create);
            options.Conventions.AuthorizePage("/Opportunities/EditModal", MyCrmPermissions.Opportunities.Edit);

            options.Conventions.AuthorizePage("/Leads/Index", MyCrmPermissions.Leads.Default);
            options.Conventions.AuthorizePage("/Leads/CreateModal", MyCrmPermissions.Leads.Create);
            options.Conventions.AuthorizePage("/Leads/EditModal", MyCrmPermissions.Leads.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);

            options.Conventions.AuthorizePage("/ServiceCategories/Index", MyCrmPermissions.ServiceCategories.Default);
            options.Conventions.AuthorizePage("/ServiceCategories/CreateModal", MyCrmPermissions.ServiceCategories.Create);
            options.Conventions.AuthorizePage("/ServiceCategories/EditModal", MyCrmPermissions.ServiceCategories.Edit);

            options.Conventions.AuthorizePage("/Contacts/Index", MyCrmPermissions.Contacts.Default);
            options.Conventions.AuthorizePage("/Contacts/CreateModal", MyCrmPermissions.Contacts.Create);
            options.Conventions.AuthorizePage("/Contacts/EditModal", MyCrmPermissions.Contacts.Edit);

            options.Conventions.AuthorizePage("/Opportunities/Index", MyCrmPermissions.Opportunities.Default);
            options.Conventions.AuthorizePage("/Opportunities/CreateModal", MyCrmPermissions.Opportunities.Create);
            options.Conventions.AuthorizePage("/Opportunities/EditModal", MyCrmPermissions.Opportunities.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);

            options.Conventions.AuthorizePage("/ServiceCategories/Index", MyCrmPermissions.ServiceCategories.Default);
            options.Conventions.AuthorizePage("/ServiceCategories/CreateModal", MyCrmPermissions.ServiceCategories.Create);
            options.Conventions.AuthorizePage("/ServiceCategories/EditModal", MyCrmPermissions.ServiceCategories.Edit);

            options.Conventions.AuthorizePage("/Contacts/Index", MyCrmPermissions.Contacts.Default);
            options.Conventions.AuthorizePage("/Contacts/CreateModal", MyCrmPermissions.Contacts.Create);
            options.Conventions.AuthorizePage("/Contacts/EditModal", MyCrmPermissions.Contacts.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);

            options.Conventions.AuthorizePage("/ServiceCategories/Index", MyCrmPermissions.ServiceCategories.Default);
            options.Conventions.AuthorizePage("/ServiceCategories/CreateModal", MyCrmPermissions.ServiceCategories.Create);
            options.Conventions.AuthorizePage("/ServiceCategories/EditModal", MyCrmPermissions.ServiceCategories.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);

            options.Conventions.AuthorizePage("/ProductCategories/Index", MyCrmPermissions.ProductCategories.Default);
            options.Conventions.AuthorizePage("/ProductCategories/CreateModal", MyCrmPermissions.ProductCategories.Create);
            options.Conventions.AuthorizePage("/ProductCategories/EditModal", MyCrmPermissions.ProductCategories.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);

            options.Conventions.AuthorizePage("/Addresses/Index", MyCrmPermissions.Addresses.Default);
            options.Conventions.AuthorizePage("/Addresses/CreateModal", MyCrmPermissions.Addresses.Create);
            options.Conventions.AuthorizePage("/Addresses/EditModal", MyCrmPermissions.Addresses.Edit);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Customers/Index", MyCrmPermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Customers/CreateModal", MyCrmPermissions.Customers.Create);
            options.Conventions.AuthorizePage("/Customers/EditModal", MyCrmPermissions.Customers.Edit);
        });

        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        if (!configuration.GetValue<bool>("App:DisablePII"))
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.LogCompleteSecurityArtifact = true;
        }

        if (!configuration.GetValue<bool>("AuthServer:RequireHttpsMetadata"))
        {
            Configure<OpenIddictServerAspNetCoreOptions>(options =>
            {
                options.DisableTransportSecurityRequirement = true;
            });
            
            Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
            });
        }

        ConfigureBundles();
        ConfigureUrls(configuration);
        ConfigureAuthentication(context);
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureSwaggerServices(context.Services);

        Configure<PermissionManagementOptions>(options =>
        {
            options.IsDynamicPermissionStoreEnabled = true;
        });
    }


    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MyCrmWebModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MyCrmWebModule>();

            if (hostingEnvironment.IsDevelopment())
            {
                options.FileSets.ReplaceEmbeddedByPhysical<MyCrmDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MyCrm.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyCrmDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MyCrm.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyCrmApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MyCrm.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyCrmApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MyCrm.Application", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyCrmHttpApiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MyCrm.HttpApi", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyCrmWebModule>(hostingEnvironment.ContentRootPath);
            }
        });
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new MyCrmMenuContributor());
        });

        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new MyCrmToolbarContributor());
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(MyCrmApplicationModule).Assembly);
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyCrm API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }


    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        app.UseCors();

        app.UseForwardedHeaders();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
            app.UseHsts();
        }

        app.UseCorrelationId();
        app.MapAbpStaticAssets();
        app.UseAbpStudioLink();
        app.UseRouting();
        app.UseAbpSecurityHeaders();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyCrm API");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}

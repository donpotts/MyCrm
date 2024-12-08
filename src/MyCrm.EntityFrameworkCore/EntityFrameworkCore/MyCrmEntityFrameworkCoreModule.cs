using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using MyCrm.Customers;
using MyCrm.Contacts;
using MyCrm.Leads;
using MyCrm.Vendors;
using MyCrm.Products;
using MyCrm.Services;

namespace MyCrm.EntityFrameworkCore;

[DependsOn(
    typeof(MyCrmDomainModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqliteModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule)
    )]
public class MyCrmEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {

        MyCrmEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MyCrmDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also MyCrmDbContextFactory for EF Core tooling. */
            options.UseSqlite();
        });
        
        context.Services.AddAlwaysDisableUnitOfWorkTransaction();
        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });

        Configure<AbpEntityOptions>(options =>
        {
            options.Entity<Customer>(customerOptions =>
            {
                customerOptions.DefaultWithDetailsFunc = query => query
                    .Include(c => c.Address)
                    .Include(c => c.Contact);
            });

            options.Entity<Contact>(contactOptions =>
            {
                contactOptions.DefaultWithDetailsFunc = query => query
                    .Include(c => c.Address)
                    .Include(c => c.Reward);
            });

            options.Entity<Lead>(leadOptions =>
            {
                leadOptions.DefaultWithDetailsFunc = query => query
                    .Include(l => l.Address)
                    .Include(l => l.Contact)
                    .Include(l => l.Opportunity);
            });

            options.Entity<Vendor>(vendorOptions =>
            {
                vendorOptions.DefaultWithDetailsFunc = query => query
                    .Include(v => v.Address)
                    .Include(v => v.Service)
                    .Include(v => v.Product);
            });

            options.Entity<Product>(productOptions =>
            {
                productOptions.DefaultWithDetailsFunc = query => query
                    .Include(p => p.ProductCategory);
            });

            options.Entity<Service>(serviceOptions =>
            {
                serviceOptions.DefaultWithDetailsFunc = query => query
                    .Include(s => s.ServiceCategory);
            });
        });
    }
}

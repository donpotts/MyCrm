using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using MyCrm.Customers;
using MyCrm.Addresses;
using MyCrm.ProductCategories;
using MyCrm.ServiceCategories;
using MyCrm.Contacts;
using MyCrm.Opportunities;
using MyCrm.Leads;
using MyCrm.Products;
using MyCrm.Services;
using MyCrm.Sales;
using MyCrm.Vendors;
using MyCrm.SupportCases;
using MyCrm.TodoTasks;
using MyCrm.Rewards;

namespace MyCrm.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class MyCrmDbContext :
    AbpDbContext<MyCrmDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Opportunity> Opportunities { get; set; }
    public DbSet<Lead> Leads { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<SupportCase> SupportCases { get; set; }
    public DbSet<TodoTask> TodoTasks { get; set; }
    public DbSet<Reward> Rewards { get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public MyCrmDbContext(DbContextOptions<MyCrmDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.Entity<Customer>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "Customers",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.Address).WithMany().HasForeignKey(x => x.AddressId);

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.Contact).WithMany().HasForeignKey(x => x.ContactId);
        });

        builder.Entity<Address>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "Addresses",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<ProductCategory>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "ProductCategories",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<ServiceCategory>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "ServiceCategories",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Contact>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "Contacts",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.Address).WithMany().HasForeignKey(x => x.AddressId);

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.Reward).WithMany().HasForeignKey(x => x.RewardId);
        });

        builder.Entity<Opportunity>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "Opportunities",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Lead>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "Leads",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.Address).WithMany().HasForeignKey(x => x.AddressId);

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.Contact).WithMany().HasForeignKey(x => x.ContactId);

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.Opportunity).WithMany().HasForeignKey(x => x.OpportunityId);
        });

        builder.Entity<Product>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "Products",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.ProductCategory).WithMany().HasForeignKey(x => x.ProductCategoryId);
        });

        builder.Entity<Service>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "Services",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.ServiceCategory).WithMany().HasForeignKey(x => x.ServiceCategoryId);
        });

        builder.Entity<Sale>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "Sales",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Vendor>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "Vendors",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.Address).WithMany().HasForeignKey(x => x.AddressId);

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.Service).WithMany().HasForeignKey(x => x.ServiceId);

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
        });

        builder.Entity<SupportCase>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "SupportCases",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<TodoTask>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "TodoTasks",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Reward>(b =>
        {
            b.ToTable(MyCrmConsts.DbTablePrefix + "Rewards",
                MyCrmConsts.DbSchema);
            b.ConfigureByConvention();
        });
    }
}

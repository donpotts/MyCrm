using MyCrm.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MyCrm.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MyCrmEntityFrameworkCoreModule),
    typeof(MyCrmApplicationContractsModule)
)]
public class MyCrmDbMigratorModule : AbpModule
{
}

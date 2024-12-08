using Volo.Abp.Modularity;

namespace MyCrm;

[DependsOn(
    typeof(MyCrmDomainModule),
    typeof(MyCrmTestBaseModule)
)]
public class MyCrmDomainTestModule : AbpModule
{

}

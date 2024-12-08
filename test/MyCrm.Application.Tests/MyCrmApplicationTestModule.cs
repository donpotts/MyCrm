using Volo.Abp.Modularity;

namespace MyCrm;

[DependsOn(
    typeof(MyCrmApplicationModule),
    typeof(MyCrmDomainTestModule)
)]
public class MyCrmApplicationTestModule : AbpModule
{

}

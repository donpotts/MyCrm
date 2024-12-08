using Volo.Abp.Modularity;

namespace MyCrm;

public abstract class MyCrmApplicationTestBase<TStartupModule> : MyCrmTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

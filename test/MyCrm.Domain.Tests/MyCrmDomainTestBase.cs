using Volo.Abp.Modularity;

namespace MyCrm;

/* Inherit from this class for your domain layer tests. */
public abstract class MyCrmDomainTestBase<TStartupModule> : MyCrmTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

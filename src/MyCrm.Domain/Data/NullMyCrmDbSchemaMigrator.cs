using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MyCrm.Data;

/* This is used if database provider does't define
 * IMyCrmDbSchemaMigrator implementation.
 */
public class NullMyCrmDbSchemaMigrator : IMyCrmDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

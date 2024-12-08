using System.Threading.Tasks;

namespace MyCrm.Data;

public interface IMyCrmDbSchemaMigrator
{
    Task MigrateAsync();
}

using MyCrm.Localization;
using Volo.Abp.Application.Services;

namespace MyCrm;

/* Inherit your application services from this class.
 */
public abstract class MyCrmAppService : ApplicationService
{
    protected MyCrmAppService()
    {
        LocalizationResource = typeof(MyCrmResource);
    }
}

using MyCrm.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MyCrm.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MyCrmController : AbpControllerBase
{
    protected MyCrmController()
    {
        LocalizationResource = typeof(MyCrmResource);
    }
}

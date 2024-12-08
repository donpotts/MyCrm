using MyCrm.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MyCrm.Web.Pages;

public abstract class MyCrmPageModel : AbpPageModel
{
    protected MyCrmPageModel()
    {
        LocalizationResourceType = typeof(MyCrmResource);
    }
}

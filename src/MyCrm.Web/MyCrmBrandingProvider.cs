using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using MyCrm.Localization;

namespace MyCrm.Web;

[Dependency(ReplaceServices = true)]
public class MyCrmBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MyCrmResource> _localizer;

    public MyCrmBrandingProvider(IStringLocalizer<MyCrmResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}

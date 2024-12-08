using Volo.Abp.Settings;

namespace MyCrm.Settings;

public class MyCrmSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MyCrmSettings.MySetting1));
    }
}

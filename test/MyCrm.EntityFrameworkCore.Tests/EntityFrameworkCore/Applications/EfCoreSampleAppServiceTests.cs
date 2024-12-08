using MyCrm.Samples;
using Xunit;

namespace MyCrm.EntityFrameworkCore.Applications;

[Collection(MyCrmTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<MyCrmEntityFrameworkCoreTestModule>
{

}

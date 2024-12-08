using MyCrm.Samples;
using Xunit;

namespace MyCrm.EntityFrameworkCore.Domains;

[Collection(MyCrmTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<MyCrmEntityFrameworkCoreTestModule>
{

}

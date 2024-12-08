using Xunit;

namespace MyCrm.EntityFrameworkCore;

[CollectionDefinition(MyCrmTestConsts.CollectionDefinitionName)]
public class MyCrmEntityFrameworkCoreCollection : ICollectionFixture<MyCrmEntityFrameworkCoreFixture>
{

}

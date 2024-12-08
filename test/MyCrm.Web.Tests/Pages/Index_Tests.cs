using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MyCrm.Pages;

[Collection(MyCrmTestConsts.CollectionDefinitionName)]
public class Index_Tests : MyCrmWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}

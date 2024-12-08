using Microsoft.AspNetCore.Builder;
using MyCrm;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("MyCrm.Web.csproj"); 
await builder.RunAbpModuleAsync<MyCrmWebTestModule>(applicationName: "MyCrm.Web");

public partial class Program
{
}

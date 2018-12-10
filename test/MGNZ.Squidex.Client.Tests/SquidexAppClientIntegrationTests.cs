namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.Threading.Tasks;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-api-integration")]
  public class SquidexAppClientIntegrationTests : BaseHandlerIntegrationTest
  {
    [Fact(Skip = "app-client is hard")]
    public async Task CreateApp_EndToEnd()
    {
      throw new NotImplementedException();
    }

    [Fact(Skip = "app-client is hard")]
    public async Task GetAllApps_EndToEnd()
    {
      throw new NotImplementedException();
    }

    [Fact(Skip = "app-client is hard")]
    public async Task CreateClient_EndToEnd()
    {
      throw new NotImplementedException();
    }

    [Fact(Skip = "app-client is hard")]
    public async Task GetAllClients_EndToEnd()
    {
      throw new NotImplementedException();
    }

    [Fact(Skip = "app-client is hard")]
    public async Task UpdateClient_EndToEnd()
    {
      throw new NotImplementedException();
    }
  }
}
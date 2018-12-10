namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Tests.Stories;

  using Xunit;

  [Obsolete]
  public class ISquidexAppClientIntegrationTests : SquidexClientIntegrationTest
  {
    [Fact(Skip = "needs refreshed admin token")]
    public async Task EndToEnd_HappyPath()
    {
      var stories = new AppStories(this.Options);

      var app1Name = $"{Guid.NewGuid().ToString("D")}-aut";
      var app2Name = $"{Guid.NewGuid().ToString("D")}-aut";

      var app1 = await stories.CreateApplication(app1Name);
      var app2 = await stories.CreateApplication(app2Name);

      var apps1 = await stories.GetApps();

      var deleteApp1 = await stories.DeleteApp(app1Name);
      var deleteApp2 = await stories.DeleteApp(app2Name);

      var apps2 = await stories.GetApps();
    }
  }
}
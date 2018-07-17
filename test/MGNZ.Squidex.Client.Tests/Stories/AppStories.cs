namespace MGNZ.Squidex.Client.Tests.Stories
{
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Common.Transport;

  public class AppStories : AuthenticatedSessionStory
  {
    public async Task<dynamic> CreateApplication(string appName)
    {
      var request = new CreateAppRequest
      {
        Name = appName
      };

      return await this.SystemUserAuthenticatedAppClient.CreateApp(request);
    }

    public async Task<dynamic> GetApps()
    {
      return await this.SystemUserAuthenticatedAppClient.GetAllApps();
    }

    public async Task<dynamic> DeleteApp(string appName)
    {
      return await this.SystemUserAuthenticatedAppClient.DeleteApp(appName);
    }

    public async Task<dynamic> CreateClient(string appName, string clientName)
    {
      return await this.SystemUserAuthenticatedAppClient.CreateClient(appName,
        new CreateClientRequest {Name = clientName});
    }

    public async Task<dynamic> GetClients(string appName)
    {
      return await this.SystemUserAuthenticatedAppClient.GetAllClients(appName);
    }

    public async Task<dynamic> UpdateClient(string appName, string clientName, string permission)
    {
      var request = new UpdateClientRequest
      {
        Perrmision = permission
      };

      return await this.SystemUserAuthenticatedAppClient.UpdateClient(appName, clientName, request);
    }
  }
}
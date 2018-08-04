namespace MGNZ.Squidex.Client.Tests.Stories
{
  using System.Net.Http;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Tests.Plumbing;
  using MGNZ.Squidex.Client.Transport;

  using Refit;

  public class AppStories : StoryBase
  {
    private ISquidexAppClient _authenticatedAppClient;

    public AppStories(TestConfigurationOptions options) : base(options)
    {
    }

    protected ISquidexAppClient GetSystemUserAuthenticatedAppClient()
    {
      return this._authenticatedAppClient ?? (this._authenticatedAppClient =
               RestService.For<ISquidexAppClient>(
                 new HttpClient(
                   new LazyAccessTokenHttpClientHandler(() => Task.FromResult(this.Options.AdministratorToken)))
                 {
                   BaseAddress = this.Options.BaseAddressUri
                 }));
    }

    public async Task<dynamic> CreateApplication(string appName)
    {
      var request = new CreateAppRequest
      {
        Name = appName
      };

      return await this.GetSystemUserAuthenticatedAppClient().CreateApp(request);
    }

    public async Task<dynamic> GetApps()
    {
      return await this.GetSystemUserAuthenticatedAppClient().GetAllApps();
    }

    public async Task<dynamic> DeleteApp(string appName)
    {
      return await this.GetSystemUserAuthenticatedAppClient().DeleteApp(appName);
    }

    public async Task<dynamic> CreateClient(string appName, string clientName)
    {
      return await this.GetSystemUserAuthenticatedAppClient().CreateClient(appName,
        new CreateClientRequest {Name = clientName});
    }

    public async Task<dynamic> GetClients(string appName)
    {
      return await this.GetSystemUserAuthenticatedAppClient().GetAllClients(appName);
    }

    public async Task<dynamic> UpdateClient(string appName, string clientName, string permission)
    {
      var request = new UpdateClientRequest
      {
        Perrmision = permission
      };

      return await this.GetSystemUserAuthenticatedAppClient().UpdateClient(appName, clientName, request);
    }
  }
}
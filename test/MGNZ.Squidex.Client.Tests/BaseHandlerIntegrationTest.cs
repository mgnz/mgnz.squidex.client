namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Net.Http;
  using System.Reflection;
  using System.Threading.Tasks;

  using Bogus;

  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Tests.Plumbing;

  using Microsoft.Extensions.Configuration;

  using Refit;

  public class BaseHandlerIntegrationTest : IDisposable
  {
    protected ISquidexAppSchemaClient SchemaClient { get; set; } = null;
    protected ISquidexContentClient ContentClient { get; } = null;
    protected ISquidexAttachmentClient AttachmentClient { get; } = null;
    protected ISquidexOAuthClient OAuthClient { get; } = null;

    protected string GetRandomName => new Faker().Random.AlphaNumeric(10).ToLower();

    public BaseHandlerIntegrationTest()
    {
      var _applicationConfiguration = new TestConfigurationOptions();
      var configurationRoot = TestHelper.GetConfigurationRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
      configurationRoot.Bind(_applicationConfiguration);

      var oauthAppName = _applicationConfiguration.Clients["aut-developer"].OAuthAppName;
      var oauthClientId = _applicationConfiguration.Clients["aut-developer"].OAuthClientId;
      var oauthClientSecret = _applicationConfiguration.Clients["aut-developer"].OAuthClientSecret;
      var baseAddress = _applicationConfiguration.BaseAddressUri;

      this.SchemaClient = this.GetOAuthSecuredClient<ISquidexAppSchemaClient>(oauthAppName, oauthClientId, oauthClientSecret, baseAddress);
      this.ContentClient = this.GetOAuthSecuredClient<ISquidexContentClient>(oauthAppName, oauthClientId, oauthClientSecret, baseAddress);
      this.AttachmentClient = this.GetOAuthSecuredClient<ISquidexAttachmentClient>(oauthAppName, oauthClientId, oauthClientSecret, baseAddress);
      this.OAuthClient = this.GetUnsecuredClient<ISquidexOAuthClient>(baseAddress);
    }

    protected TClient GetOAuthSecuredClient<TClient>(string oauthAppName, string oauthClientId, string oauthClientSecret, Uri baseAddress)
    {
      return RestService.For<TClient>(
        new HttpClient(new SimpleAccessTokenHttpClientHandler(() =>
          this.ExtractToken(oauthAppName, oauthClientId, oauthClientSecret)))
        {
          BaseAddress = baseAddress
        });
    }

    protected TClient GetUnsecuredClient<TClient>(Uri baseAddress)
    {
      return RestService.For<TClient>(baseAddress.AbsoluteUri);
    }

    private async Task<string> ExtractToken(string oauthAppName, string oauthClientId, string oauthClientSecret)
    {
      var result = await OAuthClient.GetToken(oauthAppName, oauthClientId, oauthClientSecret);

      return result.AccessToken;
    }

    public void Dispose()
    {
      Task.Run(async () => await PurgeSchema()).Wait();
      Task.Run(async () => await PurgeAttachments()).Wait();
    }

    private async Task PurgeSchema()
    {
      var data = await SchemaClient.GetAllSchemas("aut");
      var count = Convert.ToInt32(data.Count);

      if (count == 0) return;

      var allnames = ((IEnumerable<dynamic>) data).Select(d => Convert.ToString(d.name));
      foreach (var name in allnames)
        await SchemaClient.DeleteSchema("aut", name);
    }

    private async Task PurgeAttachments()
    {
      var data = await AttachmentClient.GetAllAssets("aut", new ListRequest()
      {
        Skip = 0, Top = 200
      });
      var count = data.Total;

      if (count == 0) return;

      foreach (var attachment in data.Items)
        await AttachmentClient.DeleteAsset("aut", attachment.Id);
    }
  }
}
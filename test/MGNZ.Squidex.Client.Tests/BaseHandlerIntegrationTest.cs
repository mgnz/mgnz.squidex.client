namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.IO;
  using System.Net.Http;
  using System.Reflection;
  using System.Threading.Tasks;

  using Bogus;

  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.Client.Tests.Plumbing;

  using Microsoft.Extensions.Configuration;

  using Refit;

  public class BaseHandlerIntegrationTest
  {
    protected ISquidexAppSchemaClient SchemaClient { get; set; } = null;
    protected ISquidexContentClient ContentClient { get; } = null;
    protected ISquidexAttachmentClient AttachmentClient { get; } = null;
    protected ISquidexOAuthClient OAuthClient { get; } = null;

    protected string GetRandomSchemaName => new Faker().Random.AlphaNumeric(10).ToLower();

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
  }
}
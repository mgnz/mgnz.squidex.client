using System;
using System.Collections.Generic;
using System.Text;

namespace MGNZ.Squidex.Client.Tests
{
  using System.IO;
  using System.Net.Http;
  using System.Reflection;
  using System.Threading.Tasks;

  using Bogus;

  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.Client.Tests.Plumbing;

  using Microsoft.Extensions.Configuration;

  using Refit;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-api-integration")]
  public class ISquidexAttachmentClientIntegrationTest : BaseHandlerIntegrationTest
  {
    [Fact( Skip = "in progress")]
    public async Task AttachmentPost_Execute_EndToEnd()
    {
    }

    [Fact(Skip = "in progress")]
    public async Task AttachmentPut_Execute_EndToEnd()
    {
    }

    [Fact(Skip = "in progress")]
    public async Task AttachmentTagPut_Execute_EndToEnd()
    {
    }

    [Fact(Skip = "in progress")]
    public async Task AttachmentList_Execute_EndToEnd()
    {
    }

    [Fact(Skip = "in progress")]
    public async Task AttachmentDelete_Execute_EndToEnd()
    {
    }
  }

  public class BaseHandlerIntegrationTest
  {
    protected ISquidexAppSchemaClient SchemaClient { get; set; } = null;
    protected ISquidexContentClient ContentClient { get; } = null;
    protected ISquidexAttachmentClient AttachmentClient { get; } = null;
    protected ISquidexOAuthClient OAuthClient { get; } = null;

    protected string GetRandomSchemaName => new Faker().Random.AlphaNumeric(40).ToLower();

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
          this.OAuthClient.GetToken(oauthAppName, oauthClientId, oauthClientSecret)))
        {
          BaseAddress = baseAddress
        });
    }

    protected TClient GetUnsecuredClient<TClient>(Uri baseAddress)
    {
      return RestService.For<TClient>(baseAddress.AbsolutePath);
    }
  }
}

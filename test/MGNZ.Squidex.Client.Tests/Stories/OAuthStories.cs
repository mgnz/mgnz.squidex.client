namespace MGNZ.Squidex.Client.Tests.Stories
{
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Tests.Plumbing;
  using MGNZ.Squidex.Client.Transport;

  using Refit;

  public class OAuthStories : StoryBase
  {
    private ISquidexOAuthClient _oAuthClient;

    public OAuthStories(TestConfigurationOptions options) : base(options)
    {
    }

    protected ISquidexOAuthClient GetOAuthClient()
    {
      return this._oAuthClient ?? (this._oAuthClient =
               RestService.For<ISquidexOAuthClient>(this.Options.BaseAddressUri
                 .AbsoluteUri));
    }

    public async Task<string> GetOAuthValueKnownUser(KnownUserOAuthCreds creds)
    {
      var response = await this.GetOauthTokenKnownUser(creds);
      return response.AccessToken;
    }

    public async Task<GetOAuthTokenResponse> GetOauthTokenKnownUser(KnownUserOAuthCreds creds)
    {
      return await this.GetOAuthToken(creds.OAuthAppName, creds.OAuthClientId, creds.OAuthClientSecret);
    }

    public async Task<GetOAuthTokenResponse> GetOAuthToken(string oauthAppName, string oauthClientId,
      string oauthClientSecret)
    {
      var response = await this.GetOAuthClient().GetToken(new GetOAuthTokenRequest
      {
        ClientId = $"{oauthAppName}:{oauthClientId}",
        ClientSecret = oauthClientSecret
      });

      return response;
    }
  }
}
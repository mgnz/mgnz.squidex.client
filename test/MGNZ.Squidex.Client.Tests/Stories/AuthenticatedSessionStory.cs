namespace MGNZ.Squidex.Client.Tests.Stories
{
  using System;
  using System.Net.Http;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Common.Transport;
  using MGNZ.Squidex.Client.Handlers;

  using Refit;

  public class AuthenticatedSessionStory
  {
    private ISquidexOAuthClient _plainOAuthClient = null;
    private ISquidexAppClient _authenticatedAppClient = null;
    private ISquidexAppSchemaClient _authenticatedSchemaClient = null;

    protected string BaseAddress => "https://somewhere.over.the.rainbow/";
    protected Uri BaseAddressUri => new Uri(this.BaseAddress);
   
    protected ISquidexOAuthClient PlainOAuthClient => this._plainOAuthClient ?? (this._plainOAuthClient = RestService.For<ISquidexOAuthClient>(this.BaseAddress));
    protected ISquidexAppClient AuthenticatedAppClient => this._authenticatedAppClient ?? (this._authenticatedAppClient = RestService.For<ISquidexAppClient>(new HttpClient(new AccessTokenHttpClientHandler(() => this.GetOAuthValueKnownUser(this.TempoaryHardCodedTestClientAUT))) { BaseAddress = this.BaseAddressUri }));
    protected ISquidexAppClient SystemUserAuthenticatedAppClient => this._authenticatedAppClient ?? (this._authenticatedAppClient = RestService.For<ISquidexAppClient>(new HttpClient(new AccessTokenHttpClientHandler(this.GetTokenSystemUser)) { BaseAddress = this.BaseAddressUri }));
    protected ISquidexAppSchemaClient AuthenticatedSchemaClient => this._authenticatedSchemaClient ?? (this._authenticatedSchemaClient = RestService.For<ISquidexAppSchemaClient>(new HttpClient(new AccessTokenHttpClientHandler(() => this.GetOAuthValueKnownUser(this.TempoaryHardCodedTestClientAUT))) { BaseAddress = this.BaseAddressUri }));

    public KnownUserOAuthCreds TempoaryHardCodedTestClientAUT => new KnownUserOAuthCreds
    {
      OAuthAppName = "asdf",
      OAuthClientId = "asdf-aut-testclient",
      OAuthClientSecret = "V3DJ6r8UJgulQIx0nMDWxIhGInBp7bx2DI/QvIkyUnQ="
    };

    public KnownUserOAuthCreds CurrentTestClient { get; set; }

    public async Task<string> GetTokenSystemUser()
    {
      // token for AUT user 
      return await Task.FromResult(
        "eyJhbGciOiJSUzI1NiIsImtpZCI6IjkxRkRENEVCRDYwNjMxNURFREI4MENEasdfZBMjFEREE2NkIiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJrZjNVNjlZR01WM3R1QXpRa3czdi1pSGRwbXMifQ.eyJuYmYiOjE1MzA5NDY4NzQsImV4cCI6MTUzMDk1MDQ3NCwiaXNzIjoiaHR0cDovL3BsYXlwZW4uY21zLm1hZC5nZWVrLm56L2lkZW50aXR5LXNlcnZlciIsImF1ZCI6WyJodHRwOi8vcGxheXBlbi5jbXMubWFkLmdlZWsubnovaWRlbnRpdHktc2VydmVyL3Jlc291cmNlcyIsInNxdWlkZXgtYXBpIl0sImNsaWVudF9pZCI6InNxdWlkZXgtZnJvbnRlbmQiLCJzdWIiOiI1YjBlNWM0Mzc5OWE4YjE0YjAxZWUyODMiLCJhdXRoX3RpbWUiOjE1MzA5MzI3MDEsImlkcCI6ImxvY2FsIiwiZW1haWwiOlsic3RldmVuaCthdXRAbWFkLmdlZWsubnoiLCJzdGV2ZW5oK2F1dEBtYWQuZ2Vlay5ueiJdLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiZW1haWwiLCJzcXVpZGV4LXByb2ZpbGUiLCJyb2xlIiwic3F1aWRleC1hcGkiXSwiYW1yIjpbInB3ZCJdfQ.BS7I5_fOlKrtmnoCkK8V0n5jaaf9jKAcZqwA5Sk1Gi-Kl0fpSiNQk7LNGflO0IpTEIQ5s20-xNPhUyWCFMH2rVQKHpO9JPVDfZ705RipSb6eNmCP5z9CH3-DEo4itxJRPoJcDeZucswAVhSuefz-fjOlV1hsxg_IqcywHPi56VwqOouEles9FUQzEOgPkpoYBWFI2UKU_Z6mggl8aUhTY0TbqWPTYQjNZr8q_80rW_4N4zerHpjvcSMgln8OaImuNNSDgOXIyGK_28ZBzGzAqopEIhBvQohrZh-11DZKuSMWRQYhkFMpgLxohEHGwQi8ux5O_IuMON9wt73jQbiYgg");
    }

    public async Task<string> GetOAuthValueKnownUser(KnownUserOAuthCreds creds)
    {
      var response = await this.GetOauthTokenKnownUser(creds);
      return response.AccessToken;
    }

    public async Task<SquidexGetOAuthTokenResponse> GetOauthTokenKnownUser(KnownUserOAuthCreds creds)
    {
      return await this.GetOAuthToken(creds.OAuthAppName, creds.OAuthClientId, creds.OAuthClientSecret);
    }

    public async Task<SquidexGetOAuthTokenResponse> GetOAuthToken(string oauthAppName, string oauthClientId,
      string oauthClientSecret)
    {
      var response = await this.PlainOAuthClient.GetToken(new SquidexGetOAuthTokenRequest()
      {
        ClientId = $"{oauthAppName}:{oauthClientId}",
        ClientSecret = oauthClientSecret
      });

      return response;
    }
  }
}
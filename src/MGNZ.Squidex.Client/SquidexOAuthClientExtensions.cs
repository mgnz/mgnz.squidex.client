namespace MGNZ.Squidex.Client
{
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Transport;

  public static class SquidexOAuthClientExtensions
  {
    public static async Task<string> GetToken(this ISquidexOAuthClient that, string oauthAppName, string oauthClientId, string oauthClientSecret)
    {
      // mabye just move this to the inteface

      var response = await that.GetToken(new GetOAuthTokenRequest
      {
        ClientId = $"{oauthAppName}:{oauthClientId}",
        ClientSecret = oauthClientSecret
      });

      return response.AccessToken;
    }
  }
}
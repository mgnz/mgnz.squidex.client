namespace MGNZ.Squidex.Client.Common.Transport
{
  using Newtonsoft.Json;

  public class SquidexGetOAuthTokenResponse
  {
    [JsonProperty("access_token")] public string AccessToken { get; set; }
    [JsonProperty("expires_in")] public int ExpiresIn { get; set; }
    [JsonProperty("token_type")] public string TokenType { get; set; }
  }
}
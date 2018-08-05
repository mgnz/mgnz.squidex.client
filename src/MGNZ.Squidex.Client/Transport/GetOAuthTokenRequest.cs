namespace MGNZ.Squidex.Client.Transport
{
  using Newtonsoft.Json;

  public class GetOAuthTokenRequest
  {
    [JsonProperty(PropertyName = "grant_type")] public string GrantType => "client_credentials";
    [JsonProperty(PropertyName = "client_id")] public string ClientId { get; set; }
    [JsonProperty(PropertyName = "client_secret")] public string ClientSecret { get; set; }
    [JsonProperty(PropertyName = "scope")] public string Scope => "squidex-api";
  }
}
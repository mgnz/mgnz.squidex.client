namespace MGNZ.Squidex.Client.Common.Transport
{
  using Newtonsoft.Json;

  public class UpdateClientRequest
  {
    [JsonProperty(PropertyName = "permission")] public string Perrmision { get; set; }
  }
}
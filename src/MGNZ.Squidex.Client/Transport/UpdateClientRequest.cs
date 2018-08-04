namespace MGNZ.Squidex.Client.Transport
{
  using Newtonsoft.Json;

  public class UpdateClientRequest
  {
    [JsonProperty(PropertyName = "permission")] public string Perrmision { get; set; }
  }
}
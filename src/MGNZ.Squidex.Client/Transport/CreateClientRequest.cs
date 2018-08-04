namespace MGNZ.Squidex.Client.Transport
{
  using Newtonsoft.Json;

  public class CreateClientRequest
  {
    [JsonProperty(PropertyName = "id")] public string Name { get; set; }
  }
}
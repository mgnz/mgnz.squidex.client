namespace MGNZ.Squidex.Client.Common.Transport
{
  using Newtonsoft.Json;

  public class CreateClientRequest
  {
    [JsonProperty(PropertyName = "id")] public string Name { get; set; }
  }
}
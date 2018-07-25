namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class CreateClientRequest
  {
    [JsonProperty(PropertyName = "id")] public string Name { get; set; }
  }
}
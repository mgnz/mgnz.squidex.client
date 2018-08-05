namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class ReferenceField
  {
    [JsonProperty(PropertyName = "iv")] public string[] Iv { get; set; }
  }
}
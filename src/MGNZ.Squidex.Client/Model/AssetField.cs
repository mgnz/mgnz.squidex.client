namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class AssetField
  {
    [JsonProperty(PropertyName = "iv")] public string[] Iv { get; set; }
  }
}
namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class SquidexAssetItem
  {
    [JsonProperty(PropertyName = "iv")] public string[] Iv { get; set; }
  }
}
namespace MGNZ.Squidex.Client.Common.Model
{
  using Newtonsoft.Json;

  public class SquidexAssetItem
  {
    [JsonProperty(PropertyName = "iv")] public string[] Iv { get; set; }
  }
}
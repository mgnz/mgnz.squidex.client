namespace MGNZ.Squidex.Client.Common.Model
{
  using Newtonsoft.Json;

  public class SquidexGeolocationFieldItem
  {
    [JsonProperty(PropertyName = "iv")] public SquidexGeolocationElement Iv { get; set; }
  }
}
namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class GeolocationField
  {
    [JsonProperty(PropertyName = "iv")] public GeolocationElement Iv { get; set; }
  }
}
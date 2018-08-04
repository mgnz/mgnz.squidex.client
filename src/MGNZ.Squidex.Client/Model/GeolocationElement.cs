namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class GeolocationElement
  {
    [JsonProperty(PropertyName = "latitude")] public double Latitude { get; set; }
    [JsonProperty(PropertyName = "longitude")] public double Longitude { get; set; }
  }
}
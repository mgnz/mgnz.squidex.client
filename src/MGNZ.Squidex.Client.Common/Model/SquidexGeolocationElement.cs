namespace MGNZ.Squidex.Client.Common.Model
{
  using Newtonsoft.Json;

  public class SquidexGeolocationElement
  {
    [JsonProperty(PropertyName = "latitude")] public double Latitude { get; set; }
    [JsonProperty(PropertyName = "longitude")] public double Longitude { get; set; }
  }
}
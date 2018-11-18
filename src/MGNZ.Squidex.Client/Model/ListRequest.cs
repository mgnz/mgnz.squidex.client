namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class ListRequest
  {
    [JsonProperty(PropertyName = "$skip")] public int Skip { get; set; }
    [JsonProperty(PropertyName = "$top")] public int Top { get; set; }
  }
}
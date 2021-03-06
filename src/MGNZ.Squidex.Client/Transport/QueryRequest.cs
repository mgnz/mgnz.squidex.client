namespace MGNZ.Squidex.Client.Transport
{
  using Newtonsoft.Json;

  public class QueryRequest
  {
    [JsonProperty(PropertyName = "$filter")] public string Filter { get; set; }
    [JsonProperty(PropertyName = "$orderby")] public string OrderBy { get; set; }
    [JsonProperty(PropertyName = "$search")] public string Search { get; set; }
    [JsonProperty(PropertyName = "$skip")] public int Skip { get; set; }
    [JsonProperty(PropertyName = "$top")] public int Top { get; set; }
  }
}
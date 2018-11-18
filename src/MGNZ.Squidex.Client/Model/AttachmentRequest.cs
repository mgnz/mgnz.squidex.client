namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class AttachmentRequest
  {
    [JsonProperty(PropertyName = "version")] public int Version { get; set; }
    [JsonProperty(PropertyName = "width")] public int Width { get; set; }
    [JsonProperty(PropertyName = "height")] public int Height { get; set; }
    [JsonProperty(PropertyName = "mode")] public string Mode { get; set; }

  }
}
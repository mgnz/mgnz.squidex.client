namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class AttachmentRequest
  {
    // todo : see AssetContentController.GetAssetContent
    [JsonProperty(PropertyName = "version")] public int Version { get; set; } = -1;
    [JsonProperty(PropertyName = "width")] public int? Width { get; set; }
    [JsonProperty(PropertyName = "height")] public int? Height { get; set; }
    [JsonProperty(PropertyName = "mode")] public string Mode { get; set; }

  }
}
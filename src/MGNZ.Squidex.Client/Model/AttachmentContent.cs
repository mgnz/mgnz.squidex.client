namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class AttachmentContent
  {
    [JsonProperty(PropertyName = "id")] public string Id { get; set; }
    [JsonProperty(PropertyName = "fileType")] public string FileType { get; set; }
    [JsonProperty(PropertyName = "fileName")] public string FileName { get; set; }
    [JsonProperty(PropertyName = "mimeType")] public string MimeType { get; set; }
    [JsonProperty(PropertyName = "tags")] public string[] Tags { get; set; }
    [JsonProperty(PropertyName = "fileSize")] public int FileSize { get; set; }
    [JsonProperty(PropertyName = "fileVersion")] public int FileVersion { get; set; }
    [JsonProperty(PropertyName = "isImage")] public bool IsImage { get; set; }
    [JsonProperty(PropertyName = "pixelWidth")] public int? PixelWidth { get; set; }
    [JsonProperty(PropertyName = "pixelHeight")] public int? PixelHeight { get; set; }
    [JsonProperty(PropertyName = "version")] public int Version { get; set; }
  }
}
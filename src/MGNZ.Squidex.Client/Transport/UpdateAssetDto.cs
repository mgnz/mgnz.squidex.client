namespace MGNZ.Squidex.Client.Transport
{
  using System.Collections.Generic;

  using Newtonsoft.Json;

  public class UpdateAssetDto
  {
    [JsonProperty(PropertyName = "fileName")] public string FileName { get; set; }

    [JsonProperty(PropertyName = "tags")] public string[] Tags { get; set; }
  }
}
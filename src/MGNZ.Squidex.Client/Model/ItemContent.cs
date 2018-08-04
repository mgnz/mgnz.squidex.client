namespace MGNZ.Squidex.Client.Model
{
  using System;
  using Newtonsoft.Json;

  public class ItemContent<TModel>
  {
    [JsonProperty(PropertyName = "id")] public string Id { get; set; }
    [JsonProperty(PropertyName = "data")] public TModel Data { get; set; }
    [JsonProperty(PropertyName = "version")] public int Version { get; set; }
    [JsonProperty(PropertyName = "created")] public DateTime Created { get; set; }
    [JsonProperty(PropertyName = "createdby")] public string CreatedBy { get; set; }
    [JsonProperty(PropertyName = "lastmodified")] public DateTime LastModified { get; set; }
    [JsonProperty(PropertyName = "lastmodifiedby")] public string LastModifiedBy { get; set; }
  }
}
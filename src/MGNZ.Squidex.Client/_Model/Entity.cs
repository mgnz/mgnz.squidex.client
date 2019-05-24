#if has_typed_client

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MGNZ.Squidex.Client._Model
{
  public class Entity<TModel>
  {
    [JsonProperty(PropertyName = "id")] public string Id { get; set; }
    [JsonProperty(PropertyName = "data")] public TModel Data { get; set; }
    [JsonProperty(PropertyName = "version")] public int Version { get; set; }
    [JsonProperty(PropertyName = "created")] public DateTime Created { get; set; }
    [JsonProperty(PropertyName = "createdBy")] public string CreatedBy { get; set; }
    [JsonProperty(PropertyName = "lastModified")] public DateTime LastModified { get; set; }
    [JsonProperty(PropertyName = "lastModifiedBy")] public string LastModifiedBy { get; set; }
    [JsonProperty(PropertyName = "status")] public string Status { get; set; }
    [JsonProperty(PropertyName = "isPending")] public bool IsPending { get; set; }
  }

  public class Geolocation
  {
    public double Latitude { get; set; }
    public double Longitude { get; set; }
  }
}

#endif

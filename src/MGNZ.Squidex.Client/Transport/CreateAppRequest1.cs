namespace MGNZ.Squidex.Client.Model
{
  using System;
  using Newtonsoft.Json;

  public class CreateAppRequest
  {
    [JsonProperty(PropertyName = "name")] public string Name { get; set; }
    [JsonProperty(PropertyName = "template")] public string Template => String.Empty;
  }
}
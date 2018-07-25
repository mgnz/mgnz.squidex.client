namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class SquidexReferenceItem
  {
    [JsonProperty(PropertyName = "iv")] public string[] Iv { get; set; }
  }
}
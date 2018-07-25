namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class SquidexCategoryItem
  {
    [JsonProperty(PropertyName = "iv")] public string[] Iv { get; set; }
  }
}
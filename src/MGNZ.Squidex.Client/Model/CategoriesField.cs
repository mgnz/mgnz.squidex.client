namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class CategoriesField
  {
    [JsonProperty(PropertyName = "iv")] public string[] Iv { get; set; }
  }
}
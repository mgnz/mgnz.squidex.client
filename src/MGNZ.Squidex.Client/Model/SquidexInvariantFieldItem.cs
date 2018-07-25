namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class SquidexInvariantFieldItem<T>
  {
    [JsonProperty(PropertyName = "iv")] public T Iv { get; set; }
  }
}
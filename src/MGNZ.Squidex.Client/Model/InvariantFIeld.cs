namespace MGNZ.Squidex.Client.Model
{
  using Newtonsoft.Json;

  public class InvariantField<T>
  {
    [JsonProperty(PropertyName = "iv")] public T Iv { get; set; }
  }
}
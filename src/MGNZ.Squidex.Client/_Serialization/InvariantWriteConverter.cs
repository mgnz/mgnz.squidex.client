#if has_typed_client

using Newtonsoft.Json;
using System;

namespace MGNZ.Squidex.Client._Serialization
{
  public sealed class InvariantWriteConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      writer.WriteStartObject();
      writer.WritePropertyName("iv");

      serializer.Serialize(writer, value);

      writer.WriteEndObject();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      return serializer.Deserialize(reader, objectType);
    }

    public override bool CanConvert(Type objectType)
    {
      return false;
    }
  }
}

#endif

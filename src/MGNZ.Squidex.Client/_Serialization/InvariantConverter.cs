#if has_typed_client

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Reflection;

namespace MGNZ.Squidex.Client._Serialization
{
  public sealed class InvariantConverter : JsonConverter
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
      // todo : sanity check to make sure the element we are on is actually an invariant

      reader.Read();
      reader.Read();

      var result = serializer.Deserialize(reader, objectType);

      reader.Read();

      return result;
    }

    public override bool CanConvert(Type objectType)
    {
      return false;
    }
  }

//  public class NullableValueProvider : IValueProvider
//  {
//    private readonly object _defaultValue;
//    private readonly IValueProvider _underlyingValueProvider;


//    public NullableValueProvider(MemberInfo memberInfo, Type underlyingType)
//    {
//      //_underlyingValueProvider = new DynamicValueProvider(memberInfo);
//      _underlyingValueProvider = new DynamicValueProvider(memberInfo);
//      _defaultValue = Activator.CreateInstance(underlyingType);
//    }

//    public void SetValue(object target, object value)
//    {
//      _underlyingValueProvider.SetValue(target, value);

//    }

//    public object GetValue(object target)
//    {
//      return _underlyingValueProvider.GetValue(target) ?? _defaultValue;
//    }
//  }

//  public class SpecialContractResolver : DefaultContractResolver
//  {
//    protected override IValueProvider CreateMemberValueProvider(MemberInfo member)
//    {
//      if (member.MemberType == MemberTypes.Property)
//      {
//        var pi = (PropertyInfo)member;
//        if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
//        {
//          return new NullableValueProvider(member, pi.PropertyType.GetGenericArguments().First());
//        }
//      }
//      else if (member.MemberType == MemberTypes.Field)
//      {
//        var fi = (FieldInfo)member;
//        if (fi.FieldType.IsGenericType && fi.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
//          return new NullableValueProvider(member, fi.FieldType.GetGenericArguments().First());
//      }

//      return base.CreateMemberValueProvider(member);
//    }
//  }
}

#endif

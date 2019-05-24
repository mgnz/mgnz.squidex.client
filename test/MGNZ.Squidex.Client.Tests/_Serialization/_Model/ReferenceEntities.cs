#if has_typed_client

using MGNZ.Squidex.Client._Model;
using MGNZ.Squidex.Client._Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MGNZ.Squidex.Client.Tests._Serialization._Model
{
  internal class ReferenceSerializationModel
  {
    [JsonConverter(typeof(InvariantConverter))] public string StringField { get; set; }
    public Dictionary<string, string> LocalizableStringField { get; set; }
    [JsonConverter(typeof(InvariantConverter))] public DateTime? DateTimeField { get; set; }
    [JsonConverter(typeof(InvariantConverter))] public int? NumberField { get; set; }
    [JsonConverter(typeof(InvariantConverter))] public ReferenceMultipleChildAssetModel[] ArrayField { get; set; }

    [JsonConverter(typeof(InvariantConverter))] public Guid? NullableAssetsFieldWithValue { get; set; }
    [JsonConverter(typeof(InvariantConverter))] public Guid? NullableAssetsFieldWithNoValue { get; set; }
    [JsonConverter(typeof(InvariantConverter))] public Guid NonNullableAssetFieldWithValue { get; set; }

    [JsonConverter(typeof(InvariantConverter))] public Geolocation NullableGeolocationFieldWithValue { get; set; }
    [JsonConverter(typeof(InvariantConverter))] public Geolocation NullableGeolocationFieldNoValue { get; set; }

    [JsonConverter(typeof(InvariantConverter))] public Guid NonNullableReferencesFieldWithValue { get; set; }
    [JsonConverter(typeof(InvariantConverter))] public Guid? NullableReferencesFieldWithValue { get; set; }
    [JsonConverter(typeof(InvariantConverter))] public Guid? NullableReferencesFieldNoValue { get; set; }

    [JsonConverter(typeof(InvariantConverter))] public bool NonNullableBooleanFieldWithValue { get; set; }
    [JsonConverter(typeof(InvariantConverter))] public bool? NullableBooleanFieldWithValue { get; set; }
    [JsonConverter(typeof(InvariantConverter))] public bool? NullableBooleanFieldNoValue { get; set; }

    [JsonConverter(typeof(InvariantConverter))] public dynamic JsonFieldDynamic { get; set; }
    [JsonConverter(typeof(InvariantConverter))] public string JsonFieldNull { get; set; }

    [JsonConverter(typeof(InvariantConverter))] public string[] TagsField { get; set; }
  }

  public class ReferenceElement
  {
    public Guid Iv { get; set; }
  }

  [JsonConverter(typeof(InvariantConverter))]
  public class ArrayField : List<Guid>
  {
  }

  internal class ReferenceMultipleAssetModel
  {
    public ReferenceMultipleAssetModel()
    {
      AssetsField = new List<Guid>();
    }

    [JsonProperty(PropertyName = "stringfield"), JsonConverter(typeof(InvariantConverter))] public string StringField { get; set; }
    [JsonProperty(PropertyName = "localizablestringfield")] public Dictionary<string, string> LocalizableStringField { get; set; }
    [JsonProperty(PropertyName = "datetimefield"), JsonConverter(typeof(InvariantConverter))] public DateTime? DateTimeField { get; set; }
    [JsonProperty(PropertyName = "numberfield"), JsonConverter(typeof(InvariantConverter))] public int? NumberField { get; set; }
    [JsonProperty(PropertyName = "arrayfield"), JsonConverter(typeof(InvariantConverter))] public List<ReferenceMultipleChildAssetModel> ArrayField { get; set; }
    [JsonProperty(PropertyName = "assetsfield"), JsonConverter(typeof(InvariantConverter))] public List<Guid> AssetsField { get; set; } 
    [JsonProperty(PropertyName = "geolocationfield"), JsonConverter(typeof(InvariantConverter))] public Geolocation GeolocationField { get; set; }
    [JsonProperty(PropertyName = "referencesfield")] public ArrayField ReferencesField { get; set; }
    [JsonProperty(PropertyName = "booleanfield"), JsonConverter(typeof(InvariantConverter))] public bool? BooleanField { get; set; }
    //[JsonProperty(PropertyName = "jsonfield"), JsonConverter(typeof(InvariantConverter))] public dynamic JsonField { get; set; }
    [JsonProperty(PropertyName = "tagsfield"), JsonConverter(typeof(InvariantConverter))] public List<string> TagsField { get; set; }
  }

  internal class ReferenceMultipleChildAssetModel
  {
    public string ArrayStringField1 { get; set; }
    public DateTime? ArrayDateTimeField1 { get; set; }
  }
}

#endif

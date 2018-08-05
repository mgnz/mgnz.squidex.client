namespace MGNZ.Squidex.Client.Tests.AssetModels
{
  using System;

  using MGNZ.Squidex.Client.Model;

  using Newtonsoft.Json;

  internal class ReferenceMultipleAssetModel
  {
    [JsonProperty(PropertyName = "stringfield")] public InvariantField<string> StringField { get; set; }
    [JsonProperty(PropertyName = "localizablestringfield")] public LocalizableField LocalizableStringField { get; set; }
    [JsonProperty(PropertyName = "datetimefield")] public InvariantField<DateTime> DateTimeField { get; set; }
    [JsonProperty(PropertyName = "numberfield")] public InvariantField<Int32> NumberField { get; set; }
    [JsonProperty(PropertyName = "arrayfield")] public ArrayField<ReferenceMultipleChildAssetModel> ArrayField { get; set; }
    [JsonProperty(PropertyName = "assetsfield")] public AssetField AssetsField { get; set; }
    [JsonProperty(PropertyName = "geolocationfield")] public GeolocationField GeolocationField { get; set; }
    [JsonProperty(PropertyName = "referencesfield")] public ReferenceField ReferencesField { get; set; }
    [JsonProperty(PropertyName = "booleanfield")] public InvariantField<bool> BooleanField { get; set; }
    //[JsonProperty(PropertyName = "stringfield")] public InvariantField<bool> JsonField { get; set; }
    [JsonProperty(PropertyName = "tagsfield")] public CategoriesField TagsField { get; set; }

  }

  internal class ReferenceMultipleChildAssetModel
  {
    [JsonProperty(PropertyName = "arraystringfield1")] public InvariantField<string> ArrayStringField1 { get; set; }
    [JsonProperty(PropertyName = "arraydatetimefield1")] public InvariantField<DateTime> ArrayDateTimeField1 { get; set; }
  }
}
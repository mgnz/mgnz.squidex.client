namespace MGNZ.Squidex.Client.Tests.AssetModels
{
  using System;

  using MGNZ.Squidex.Client.Model;

  using Newtonsoft.Json;

  internal class ReferenceMultipleAssetModel
  {
    [JsonProperty(PropertyName = "stringfield")] public SquidexInvariantFieldItem<string> StringField { get; set; }
    [JsonProperty(PropertyName = "localizablestringfield")] public SquidexLanguagetFieldItem LocalizableStringField { get; set; }
    [JsonProperty(PropertyName = "datetimefield")] public SquidexInvariantFieldItem<DateTime> DateTimeField { get; set; }
    [JsonProperty(PropertyName = "numberfield")] public SquidexInvariantFieldItem<Int32> NumberField { get; set; }
    [JsonProperty(PropertyName = "arrayfield")] public SquidexArrayFieldItem<ReferenceMultipleChildAssetModel> ArrayField { get; set; }
    [JsonProperty(PropertyName = "assetsfield")] public SquidexAssetItem AssetsField { get; set; }
    [JsonProperty(PropertyName = "geolocationfield")] public SquidexGeolocationFieldItem GeolocationField { get; set; }
    [JsonProperty(PropertyName = "referencesfield")] public SquidexReferenceItem ReferencesField { get; set; }
    [JsonProperty(PropertyName = "booleanfield")] public SquidexInvariantFieldItem<bool> BooleanField { get; set; }
    //[JsonProperty(PropertyName = "stringfield")] public SquidexInvariantFieldItem<bool> JsonField { get; set; }
    [JsonProperty(PropertyName = "tagsfield")] public SquidexCategoryItem TagsField { get; set; }

  }

  internal class ReferenceMultipleChildAssetModel
  {
    [JsonProperty(PropertyName = "arraystringfield1")] public SquidexInvariantFieldItem<string> ArrayStringField1 { get; set; }
    [JsonProperty(PropertyName = "arraydatetimefield1")] public SquidexInvariantFieldItem<DateTime> ArrayDateTimeField1 { get; set; }
  }
}
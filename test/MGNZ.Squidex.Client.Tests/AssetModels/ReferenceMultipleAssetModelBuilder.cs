namespace MGNZ.Squidex.Client.Tests.AssetModels
{
  using System;

  using MGNZ.Squidex.Client.Common.Model;

  internal class ReferenceMultipleAssetModelBuilder
  {
    public static Lazy<ReferenceMultipleAssetModel> Reference1 => new Lazy<ReferenceMultipleAssetModel>(ValueFactory($"Reference 1 {DateTime.Now.ToShortTimeString()}"));
    public static Lazy<ReferenceMultipleAssetModel> Reference2 => new Lazy<ReferenceMultipleAssetModel>(ValueFactory($"Reference 2 {DateTime.Now.ToShortTimeString()}"));

    public static ReferenceMultipleAssetModel PutReference(ReferenceMultipleAssetModel from)
    {
      var putted = from;
      putted.StringField.Iv = "updated in put";

      return putted;
    }

    public static ReferenceMultipleAssetModel PatchReference()
    {
      return new ReferenceMultipleAssetModel { StringField = { Iv = "updated in patch" } };
    }

    private static ReferenceMultipleAssetModel ValueFactory(string seed, ReferenceMultipleAssetModel[] references = null)
    {
      return new ReferenceMultipleAssetModel()
      {
        StringField = new SquidexInvariantFieldItem<string>() {Iv = $"{seed} stringfield"},
        LocalizableStringField = new SquidexLanguagetFieldItem() {{"en", $"{seed} language item en"}, {"sm", $"{seed} language item sm"}},
        DateTimeField = new SquidexInvariantFieldItem<DateTime>() {Iv = DateTime.Now.AddHours(-20)},
        NumberField = new SquidexInvariantFieldItem<Int32>() {Iv = seed.GetHashCode()},
        ArrayField = new SquidexArrayFieldItem<ReferenceMultipleChildAssetModel>()
        {
          Iv = new[ ]
          {
            new ReferenceMultipleChildAssetModel()
            {
              //ArrayDateTimeField1 = new SquidexInvariantFieldItem<DateTime>() {Iv = DateTime.Now},
              ArrayStringField1 = new SquidexInvariantFieldItem<string>() {Iv = "blah"}
            },
            new ReferenceMultipleChildAssetModel()
            {
              //ArrayDateTimeField1 = new SquidexInvariantFieldItem<DateTime>() {Iv = DateTime.Now},
              ArrayStringField1 = new SquidexInvariantFieldItem<string>() {Iv = "blah"}
            },
          }
        },
        //AssetsField = new SquidexAssetItem() { Iv = new[] { "" } },
        GeolocationField = new SquidexGeolocationFieldItem() {Iv = new SquidexGeolocationElement() {Latitude = 66.6, Longitude = 77.7}},
        //ReferencesField = new SquidexReferenceItem() { Iv = new[] { "" } },
        BooleanField = new SquidexInvariantFieldItem<bool>() {Iv = true},
        TagsField = new SquidexCategoryItem() {Iv = new[ ]
        {
          $"{seed} tag a", $"{seed} tag b", $"{seed} tag c"

        }}
      };
    }
  }
}
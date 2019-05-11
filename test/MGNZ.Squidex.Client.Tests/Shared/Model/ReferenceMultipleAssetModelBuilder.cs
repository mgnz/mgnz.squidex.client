using MGNZ.Squidex.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MGNZ.Squidex.Client.Tests.Shared.Model
{
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
        StringField = new InvariantField<string>() { Iv = $"{seed} stringfield" },
        LocalizableStringField = new LocalizableField() { { "en", $"{seed} language item en" }, { "sm", $"{seed} language item sm" } },
        DateTimeField = new InvariantField<DateTime>() { Iv = DateTime.Now.AddHours(-20) },
        NumberField = new InvariantField<Int32>() { Iv = seed.GetHashCode() },
        ArrayField = new ArrayField<ReferenceMultipleChildAssetModel>()
        {
          Iv = new[]
          {
            new ReferenceMultipleChildAssetModel()
            {
              //ArrayDateTimeField1 = new InvariantField<DateTime>() {Iv = DateTime.Now},
              ArrayStringField1 = new InvariantField<string>() {Iv = "blah"}
            },
            new ReferenceMultipleChildAssetModel()
            {
              //ArrayDateTimeField1 = new InvariantField<DateTime>() {Iv = DateTime.Now},
              ArrayStringField1 = new InvariantField<string>() {Iv = "blah"}
            },
          }
        },
        //AssetsField = new AssetField() { Iv = new[] { "" } },
        GeolocationField = new GeolocationField() { Iv = new GeolocationElement() { Latitude = 66.6, Longitude = 77.7 } },
        //ReferencesField = new ReferenceField() { Iv = new[] { "" } },
        BooleanField = new InvariantField<bool>() { Iv = true },
        TagsField = new CategoriesField()
        {
          Iv = new[]
        {
          $"{seed} tag a", $"{seed} tag b", $"{seed} tag c"

        }
        }
      };
    }
  }
}

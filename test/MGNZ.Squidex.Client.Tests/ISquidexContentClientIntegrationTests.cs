namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Tests.AssetModels;
  using MGNZ.Squidex.Client.Tests.Stories;
  using MGNZ.Squidex.Client.Transport;

  using Newtonsoft.Json;

  using Xunit;

  // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1&tabs=basicconfiguration
  // https://blogs.msdn.microsoft.com/fkaduk/2017/02/22/using-strongly-typed-configuration-in-net-core-console-app/

  public class ISquidexContentClientIntegrationTests : SquidexClientIntegrationTest
  {
    [Fact]
    public async Task EndToEnd_HappyPath()
    {
      JsonConvert.DefaultSettings = () => new JsonSerializerSettings
      {
        DateFormatString = "yyyy-MM-ddTHH:mm:ssZ"
      };
      var schemaStories = new SchemaStories(this.Options);
      var contentStories = new ContentStories(this.Options);

      {
        // assert no schemas on the AUT application
        var all = await schemaStories.GetSchemas("aut");
        int count = Convert.ToInt32(all.Count);
        count.Should().Be(0, "Test cannot start because there are unexpected Schemas on the target endpoint");

        // create schema
        var referenceSchema = this.Schema1Asset.Value;
        referenceSchema.name = "schema1name";
        var created = await schemaStories.PostSchema("aut", referenceSchema);
        var published = await schemaStories.PublishSchema("aut", "schema1name");
      }

      var record1Expected = ReferenceMultipleAssetModelBuilder.Reference1.Value;
      var record2Expected = ReferenceMultipleAssetModelBuilder.Reference2.Value;

      var record1Id = string.Empty;
      var record2Id = string.Empty;

      // test insert things
      {
        var inserted1Actual = await contentStories.Create("aut", "schema1name", record1Expected);
        record1Id = AssertReference(inserted1Actual, record1Expected, "inserting things");


        var inserted2Actual = await contentStories.Create("aut", "schema1name", record2Expected);
        record2Id = AssertReference(inserted2Actual, record2Expected, "inserting things");
      }

      // publish the things
      {
        await contentStories.Publish("aut", "schema1name", record1Id);
        await contentStories.Publish("aut", "schema1name", record2Id);
      }

      // test get things
      {
        var get1byId = await contentStories.Get<ReferenceMultipleAssetModel>("aut", "schema1name", record1Id);
        AssertReference(get1byId, record1Expected, "getting things");

        var get2byId = await contentStories.Get<ReferenceMultipleAssetModel>("aut", "schema1name", record2Id);
        AssertReference(get2byId, record2Expected, "getting things");
      }

      // test put the things
      {
        var updated1 = ReferenceMultipleAssetModelBuilder.PutReference(record1Expected);
        var update1byId = await contentStories.Put("aut", "schema1name", record1Id, updated1);
        AssertReference(update1byId, record1Expected, "put things");

        //var updated2 = ReferenceMultipleAssetModelBuilder.PutReference(record2Expected);
        //var update2byId = await stories.Put("aut", "schema1name", record2Id, updated2);
        //AssertReference(update2byId, record2Expected, "put things");
      }

      // test patch the things
      {
        var patched1expected = new ReferenceMultipleAssetModel { StringField = new InvariantField<string>() { Iv = "updated in patch" } };
        var patched1byId = await contentStories.Patch("aut", "schema1name", record1Id, patched1expected);
        patched1byId.StringField.Iv.Should().Be(patched1expected.StringField.Iv, because: $"at stage patch things");

        //var patched2expected = new ReferenceMultipleAssetModel { StringField = new InvariantField<string>() { Iv = "updated in patch" } };
        //var patched2byId = await stories.Patch("aut", "schema1name", record2Id, patched2expected);
        //patched2byId.StringField.Iv.Should().Be(patched2expected.StringField.Iv, because: $"at stage patch things");
      }

      // query
      {
        var request = SquidexQueryRequestBuilder.Build(requestedFilter: $"data/stringfield/iv eq '{record2Expected.StringField.Iv}'");
        //var query = await stories.Query<ReferenceMultipleAssetModel>("aut", "schema1name", request);
        var query = await contentStories.Query<ReferenceMultipleAssetModel>("aut", "schema1name",
          request.Top, request.Skip, request.OrderBy, request.Search, request.Filter);

      }

      // unpublish
      {
        await contentStories.Unpublish("aut", "schema1name", record1Id);
      }

      // delete
      {
        await contentStories.Delete("aut", "schema1name", record1Id);
      }

      // archive
      {
        await contentStories.Archive("aut", "schema1name", record2Id);
      }

      // restore
      {
        await contentStories.Restore("aut", "schema1name", record2Id);
      }

      // query

      {
        // clean up
        await schemaStories.DeleteSchema("aut", "schema1name");
        var all = await schemaStories.GetSchemas("aut");
        int count = Convert.ToInt32(all.Count);
        count.Should().Be(0);
      }
    }

    private static void AssertReference(ReferenceMultipleAssetModel inserted1Actual, ReferenceMultipleAssetModel record1Expected, string asserting)
    {
      inserted1Actual.StringField.Iv.Should().Be(record1Expected.StringField.Iv, because: $"at stage {asserting}");
      inserted1Actual.BooleanField.Iv.Should().Be(record1Expected.BooleanField.Iv, because: $"at stage {asserting}");
      inserted1Actual.DateTimeField.Iv.Should().BeCloseTo(record1Expected.DateTimeField.Iv, because: $"at stage {asserting}", precision: TimeSpan.FromSeconds(5));
      inserted1Actual.NumberField.Iv.Should().Be(record1Expected.NumberField.Iv, because: $"at stage {asserting}");
    }

    private static string AssertReference(ItemContent<ReferenceMultipleAssetModel> inserted1Actual, ReferenceMultipleAssetModel record1Expected, string asserting)
    {
      AssertReference(inserted1Actual.Data, record1Expected, asserting);
      inserted1Actual.Id.Should().NotBeNullOrEmpty(because: $"at stage {asserting}").And.NotBeNullOrWhiteSpace(because: $"at stage {asserting}");

      return inserted1Actual.Id;
    }
  }
}
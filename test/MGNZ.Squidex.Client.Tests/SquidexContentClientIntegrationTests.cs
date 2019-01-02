namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Tests.Shared.Assets;
  using MGNZ.Squidex.Client.Tests.Shared.Code;
  using MGNZ.Squidex.Client.Transport;

  using Xunit;

  // currently there is no way to determine if a given record is in the following states
  // - published, archived
  // so while we test against publish/unpublish, archive/restore we dont verify that the state took

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-api-integration")]
  public class SquidexContentClientIntegrationTests : BaseHandlerIntegrationTest
  {
    [Fact]
    public async Task Query_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      ItemContent<dynamic> create1response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data1Post);
      ItemContent<dynamic> create2response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data2Post);
      await ContentClient.Publish("aut", schemaName, create1response.Id);
      await ContentClient.Publish("aut", schemaName, create2response.Id);

      // act

      // note : eventual consistency and all that sometimes we don't get away with validating right away.

      await Task.Delay(TimeSpan.FromSeconds(1));

      //var content = await ContentClient.Query<dynamic>("aut", schemaName, new QueryRequest()
      //{
      //  Skip = 0,
      //  Top = 100
      //});
      var content = await ContentClient.Query("aut", schemaName, top: 100, skip: 0);

      content.Total.Should().Be(2);
      var actualFirst = content.Items[0];
      var actualSecond = content.Items[1];

      // todo : verify export content

      await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact]
    public async Task Create_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      // act

      dynamic create1response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data1Post);
      dynamic create2response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data2Post);
      string create1id = Convert.ToString(create1response.id);
      string create2id = Convert.ToString(create2response.id);
      await ContentClient.Publish("aut", schemaName, create1id);
      await ContentClient.Publish("aut", schemaName, create2id);

      // note : eventual consistency and all that sometimes we don't get away with validating right away.

      await Task.Delay(TimeSpan.FromSeconds(1));

      await ContentClient.AssertContentMustExists("aut", schemaName, create1id, delay: TimeSpan.FromSeconds(0.5));
      await ContentClient.AssertContentMustExists("aut", schemaName, create2id, delay: TimeSpan.FromSeconds(0.5));

      // todo : verify export content

      await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact]
    public async Task Get_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      ItemContent<dynamic> create1response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data1Post);
      ItemContent<dynamic> create2response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data2Post);
      await ContentClient.Publish("aut", schemaName, create1response.Id);
      await ContentClient.Publish("aut", schemaName, create2response.Id);

      // act

      var get1response = await ContentClient.Get<dynamic>("aut", schemaName, create1response.Id);
      var get2response = await ContentClient.Get<dynamic>("aut", schemaName, create2response.Id);

      // todo : verify export content 

      await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact]
    public async Task Put_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      ItemContent<dynamic> create1response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data1Post);
      var patch1response = await ContentClient.Put("aut", schemaName, create1response.Id, AssetLoader.Schema1Data2Post);

      // clean up

      await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact]
    public async Task Patch_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      ItemContent<dynamic> create1response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data1Post);
      var patch1response = await ContentClient.Patch("aut", schemaName, create1response.Id, AssetLoader.Schema1Data2Post);

      // clean up

      await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact]
    public async Task Publish_EndToEnd()
    {
      // Query only 'sees' published records so that is a quick way to determine publish state
      // - insert some records
      // - assert that there are none (because we havent published them)
      // - publish them
      // - assert there are two (because we published two)

      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      ItemContent<dynamic> create1response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data1Post);
      ItemContent<dynamic> create2response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data2Post);

      // act

      // note : eventual consistency and all that sometimes we don't get away with validating right away.

      await Task.Delay(TimeSpan.FromSeconds(1));

      await ContentClient.Publish("aut", schemaName, create1response.Id);
      await ContentClient.Publish("aut", schemaName, create2response.Id);

      await Task.Delay(TimeSpan.FromSeconds(1));

      await ContentClient.AssertContentMustExists("aut", schemaName, create1response.Id, delay: TimeSpan.FromSeconds(0.5));
      await ContentClient.AssertContentMustExists("aut", schemaName, create2response.Id, delay: TimeSpan.FromSeconds(0.5));

      // clean up

      await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact]
    public async Task Unpublish_EndToEnd()
    {
      // Query only 'sees' published records so that is a quick way to determine publish state
      // - insert some records
      // - publish them
      // - un publish them
      // - assert there are none (because we unpublished them)

      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      var create1response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data1Post);
      var create2response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data2Post);

      await Task.Delay(TimeSpan.FromSeconds(1));
      await ContentClient.Publish("aut", schemaName, create1response.Id);
      await ContentClient.Publish("aut", schemaName, create2response.Id);

      // act

      // note : eventual consistency and all that sometimes we don't get away with validating right away.

      await Task.Delay(TimeSpan.FromSeconds(1));

      await ContentClient.Unpublish("aut", schemaName, create1response.Id);
      await ContentClient.Unpublish("aut", schemaName, create2response.Id);

      await Task.Delay(TimeSpan.FromSeconds(1));

      //var content = await ContentClient.Query<dynamic>("aut", schemaName, new QueryRequest()
      //{
      //  Skip = 0,
      //  Top = 100
      //});
      var content = await ContentClient.Query("aut", schemaName, top: 100, skip: 0);

      content.Total.Should().Be(0);

      // clean up

      await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact(Skip = "cant validate a archived item")]
    public async Task Archive_EndToEnd()
    {
      // Query only 'sees' published records so that is a quick way to determine publish state
      // - insert some records
      // - publish them
      // - archive them
      // - assert there are none (because we archived them)

      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      var create1response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data1Post);
      var create2response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data2Post);

      await Task.Delay(TimeSpan.FromSeconds(1));
      await ContentClient.Publish("aut", schemaName, create1response.Id);
      await ContentClient.Publish("aut", schemaName, create2response.Id);

      // act

      // note : eventual consistency and all that sometimes we don't get away with validating right away.

      await Task.Delay(TimeSpan.FromSeconds(1));

      await ContentClient.Archive("aut", schemaName, create1response.Id);
      await ContentClient.Archive("aut", schemaName, create2response.Id);

      await Task.Delay(TimeSpan.FromSeconds(1));

      //var content = await ContentClient.Query<dynamic>("aut", schemaName, new QueryRequest()
      //{
      //  Skip = 0,
      //  Top = 100
      //});
      var content = await ContentClient.Query("aut", schemaName, top: 100, skip: 0);

      content.Total.Should().Be(0);

      // clean up

      await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact(Skip = "cant validate a restored item")]
    public async Task Restore_EndToEnd()
    {
      // Query only 'sees' published records so that is a quick way to determine publish state
      // - insert some records
      // - publish them
      // - archive them
      // - assert there are none (because we archived them)

      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      var create1response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data1Post);
      var create2response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data2Post);

      await Task.Delay(TimeSpan.FromSeconds(1));
      await ContentClient.Publish("aut", schemaName, create1response.Id);
      await ContentClient.Publish("aut", schemaName, create2response.Id);
      await Task.Delay(TimeSpan.FromSeconds(1));
      await ContentClient.Archive("aut", schemaName, create1response.Id);
      await ContentClient.Archive("aut", schemaName, create2response.Id);

      // act

      // note : eventual consistency and all that sometimes we don't get away with validating right away.

      await Task.Delay(TimeSpan.FromSeconds(1));

      await ContentClient.Restore("aut", schemaName, create1response.Id);
      await ContentClient.Restore("aut", schemaName, create2response.Id);

      await Task.Delay(TimeSpan.FromSeconds(1));

      //var content = await ContentClient.Query<dynamic>("aut", schemaName, new QueryRequest()
      //{
      //  Skip = 0,
      //  Top = 100
      //});
      var content = await ContentClient.Query("aut", schemaName, top: 100, skip: 0);

      // ma
      content.Total.Should().Be(2);

      // clean up

      await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact]
    public async Task Delete_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      ItemContent<dynamic> create1response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data1Post);
      ItemContent<dynamic> create2response = await ContentClient.Post("aut", schemaName, AssetLoader.Schema1Data2Post);
      await ContentClient.Publish("aut", schemaName, create1response.Id);
      await ContentClient.Publish("aut", schemaName, create2response.Id);

      // act

      await ContentClient.Delete("aut", schemaName, create1response.Id);
      await ContentClient.Delete("aut", schemaName, create2response.Id);

      // note : eventual consistency and all that sometimes we don't get away with validating right away.

      await Task.Delay(TimeSpan.FromSeconds(1));

      await ContentClient.AssertContentMustNotExists("aut", schemaName, create1response.Id, delay: TimeSpan.FromSeconds(0.5));
      await ContentClient.AssertContentMustNotExists("aut", schemaName, create2response.Id, delay: TimeSpan.FromSeconds(0.5));

      // todo : verify export content

      await SchemaClient.DeleteSchema("aut", schemaName);
    }
  }
}
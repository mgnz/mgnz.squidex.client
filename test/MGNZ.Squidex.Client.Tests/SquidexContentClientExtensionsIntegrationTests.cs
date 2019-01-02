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

  [Collection("Sequential Squidex Integration Tests")]
  //[Trait("category", "squidex-api-integration")]
  [Trait("category", "developmet")]
  public class SquidexContentClientExtensionsIntegrationTests : BaseHandlerIntegrationTest
  {
    [Fact(Skip = "in progress")]
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

      var content = await ContentClient.Query<dynamic>("aut", schemaName, new QueryRequest()
      {
        Skip = 0,
        Top = 100
      });

      content.Total.Should().Be(2);
      var actualFirst = content.Items[0];
      var actualSecond = content.Items[1];

      // todo : verify export content

      await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact(Skip = "in progress")]
    public async Task Query2_EndToEnd()
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
      var content = await ContentClient.Query<dynamic>("aut", schemaName, top: 100, skip: 0);

      content.Total.Should().Be(2);
      var actualFirst = content.Items[0];
      var actualSecond = content.Items[1];

      // todo : verify export content

      await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact(Skip = "in progress")]
    public async Task Create_EndToEnd()
    {
      Assert.False(false);
    }

    [Fact(Skip = "in progress")]
    public async Task Get_EndToEnd()
    {
      Assert.False(false);
    }

    [Fact(Skip = "in progress")]
    public async Task Update_EndToEnd()
    {
      Assert.False(false);
    }
  }
}
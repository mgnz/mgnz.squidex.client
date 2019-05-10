namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Transport;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  //[Trait("category", "squidex-api-integration")]
  [Trait("category", "developmet")]
  public class SquidexContentClientExtensionsIntegrationTests : BaseHandlerIntegrationTest
  {
    [Fact(Skip = "typesafe is broken for array child elements")]
    public Task Query_EndToEnd()
    {
      throw new NotImplementedException();

      //var schemaName = GetRandomSchemaName;
      //await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      //var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      //var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      //dynamic create1response = await ContentClient.Post("aut", schemaName, AssetLoader.AsDynamic(AssetLoader.Schema1Data1PostName));
      //dynamic create2response = await ContentClient.Post("aut", schemaName, AssetLoader.AsDynamic(AssetLoader.Schema1Data2PostName));
      //string create1id = Convert.ToString(create1response.id);
      //string create2id = Convert.ToString(create2response.id);
      //await ContentClient.Publish("aut", schemaName, create1id);
      //await ContentClient.Publish("aut", schemaName, create2id);

      //// act

      //// note : eventual consistency and all that sometimes we don't get away with validating right away.

      //await Task.Delay(TimeSpan.FromSeconds(1));
      //var content = await ContentClient.Query<ReferenceMultipleAssetModel>("aut", schemaName, new QueryRequest()
      //{
      //  Skip = 0,
      //  Top = 100
      //});

      //content.Total.Should().Be(2);
      //var actualFirst = content.Items[0];
      //var actualSecond = content.Items[1];

      //// todo : verify export content

      //await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact(Skip = "typesafe is broken for array child elements")]
    public Task Query2_EndToEnd()
    {
      throw new NotImplementedException();

      //var schemaName = GetRandomSchemaName;
      //await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      //var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      //var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      //ItemContent<dynamic> create1response = await ContentClient.Post("aut", schemaName, AssetLoader.AsDynamic(AssetLoader.Schema1Data1PostName));
      //ItemContent<dynamic> create2response = await ContentClient.Post("aut", schemaName, AssetLoader.AsDynamic(AssetLoader.Schema1Data2PostName));
      //await ContentClient.Publish("aut", schemaName, create1response.Id);
      //await ContentClient.Publish("aut", schemaName, create2response.Id);

      //// act

      //// note : eventual consistency and all that sometimes we don't get away with validating right away.

      //await Task.Delay(TimeSpan.FromSeconds(1));

      ////var content = await ContentClient.Query<dynamic>("aut", schemaName, new QueryRequest()
      ////{
      ////  Skip = 0,
      ////  Top = 100
      ////});
      //var content = await ContentClient.Query<dynamic>("aut", schemaName, top: 100, skip: 0);

      //content.Total.Should().Be(2);
      //var actualFirst = content.Items[0];
      //var actualSecond = content.Items[1];

      //// todo : verify export content

      //await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact(Skip = "typesafe is broken for array child elements")]
    public Task Create_EndToEnd()
    {
      throw new NotImplementedException();

      //var schemaName = GetRandomSchemaName;
      //await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      //var createschema = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      //var publishedschema = await SchemaClient.PublishSchema("aut", schemaName);

      //// act

      //var create1response = await ContentClient.Create<dynamic>("aut", schemaName, AssetLoader.AsDynamic(AssetLoader.Schema1Data1PostName));
      //var create2response = await ContentClient.Create<dynamic>("aut", schemaName, AssetLoader.AsDynamic(AssetLoader.Schema1Data2PostName));
      //await ContentClient.Publish("aut", schemaName, create1response.Id);
      //await ContentClient.Publish("aut", schemaName, create2response.Id);

      //// note : eventual consistency and all that sometimes we don't get away with validating right away.

      //await Task.Delay(TimeSpan.FromSeconds(1));

      //await ContentClient.AssertContentMustExists("aut", schemaName, create1response.Id, delay: TimeSpan.FromSeconds(0.5));
      //await ContentClient.AssertContentMustExists("aut", schemaName, create2response.Id, delay: TimeSpan.FromSeconds(0.5));

      //// todo : verify export content

      //await SchemaClient.DeleteSchema("aut", schemaName);
    }

    [Fact(Skip = "typesafe is broken for array child elements")]
    public Task Get_EndToEnd()
    {
      throw new NotImplementedException();
    }

    [Fact(Skip = "typesafe is broken for array child elements")]
    public Task Update_EndToEnd()
    {
      throw new NotImplementedException();
    }
  }
}
namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.Threading.Tasks;

  using FluentAssertions;
  using MGNZ.Squidex.Tests.Shared.Assets;
  using MGNZ.Squidex.Tests.Shared.Code;
  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-api-integration")]
  public class SquidexSchemaClientIntegrationTests : BaseHandlerIntegrationTest
  {
    [Fact]
    public async Task CreateSchema_EndToEnd()
    {
      var schemaName = GetRandomName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));

      var createresult = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      await SchemaClient.AssertSchemaMustExist("aut", schemaName, TimeSpan.FromSeconds(0.5));

      var deleteresult = await SchemaClient.DeleteSchema("aut", schemaName);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task DeleteSchema_EndToEnd()
    {
      var schemaName = GetRandomName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var createresult = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));

      var deleteresult = await SchemaClient.DeleteSchema("aut", schemaName);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task GetAllSchemas_EndToEnd()
    {
      var schema1name = GetRandomName;
      var schema2name = GetRandomName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var create1result = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schema1name));
      var create2result = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schema2name));

      var all = await SchemaClient.GetAllSchemas("aut");
      int count = Convert.ToInt32(all.Count);
      count.Should().Be(2);

      var delete1result = await SchemaClient.DeleteSchema("aut", schema1name);
      var delete2result = await SchemaClient.DeleteSchema("aut", schema2name);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task GetSchema_EndToEnd()
    {
      var schema1name = GetRandomName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var create1result = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schema1name));

      var that = await SchemaClient.GetSchema("aut", schema1name);
      //int count = Convert.ToInt32(that.Count);
      //count.Should().Be(2);

      var delete1result = await SchemaClient.DeleteSchema("aut", schema1name);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task PublishSchema_EndToEnd()
    {
      var schema1name = GetRandomName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var create1result = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schema1name));

      var result = await SchemaClient.PublishSchema("aut", schema1name);

      //int count = Convert.ToInt32(that.Count);
      //count.Should().Be(2);

      var delete1result = await SchemaClient.DeleteSchema("aut", schema1name);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task UnpublishSchema_EndToEnd()
    {
      var schema1name = GetRandomName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var create1result = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schema1name));
      var publishresult = await SchemaClient.PublishSchema("aut", schema1name);

      var unpublishresult = await SchemaClient.UnpublishSchema("aut", schema1name);

      //int count = Convert.ToInt32(that.Count);
      //count.Should().Be(2);

      var delete1result = await SchemaClient.DeleteSchema("aut", schema1name);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact(Skip = "in progress")]
    public Task UpdateScripts_EndToEnd()
    {
      throw new NotImplementedException();
    }
  }
}
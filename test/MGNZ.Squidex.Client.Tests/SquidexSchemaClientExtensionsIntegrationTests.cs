namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client.Tests.Shared.Assets;
  using MGNZ.Squidex.Client.Tests.Shared.Code;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-api-integration")]
  public class SquidexSchemaClientExtensionsIntegrationTests : BaseHandlerIntegrationTest
  {
    [Fact]
    public async Task SchemaExists_EndToEnd()
    {
      var schemaName = GetRandomName;

      var nothingExists = await SchemaClient.SchemaExists("aut");
      nothingExists.Should().BeFalse();

      var somethingDoesNotExist = await SchemaClient.SchemaExists("aut", schemaName);
      somethingDoesNotExist.Should().BeFalse();

      var createresult = await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(schemaName));
      var somethingDefinatelyExist = await SchemaClient.SchemaExists("aut", schemaName);
      somethingDefinatelyExist.Should().BeTrue();

      var deleteresult = await SchemaClient.DeleteSchema("aut", schemaName);
    }
  }
}
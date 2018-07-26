namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.IO;
  using System.Net.Http;
  using System.Reflection;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.Client.Tests.AssetModels;
  using MGNZ.Squidex.Client.Tests.Plumbing;
  using MGNZ.Squidex.Client.Tests.Stories;

  using Microsoft.Extensions.Configuration;

  using Newtonsoft.Json;

  using Refit;

  using Xunit;

  // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1&tabs=basicconfiguration
  // https://blogs.msdn.microsoft.com/fkaduk/2017/02/22/using-strongly-typed-configuration-in-net-core-console-app/

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-api-integration")]
  public class SquidexClientIntegrationTestBase : ConfigurationAwareTestBase
  {
    private TestConfigurationOptions _options;
    private ISquidexAppSchemaClient _authenticatedSchemaClient;
    private ISquidexContentClient _authenticatedContentClient;
    private ISquidexOAuthClient _plainOAuthClient;
    private Lazy<dynamic> _schemaAsset;

    private TestConfigurationOptions Options
    {
      get
      {
        if (this._options == null)
        {
          this._options = new TestConfigurationOptions();
          var config = base.GetConfigurationRoot();
          config.GetSection("options").Bind(this._options);
        }

        return this._options;
      }
    }

    protected Lazy<dynamic> Schema1Asset => this._schemaAsset ?? (this._schemaAsset = new Lazy<dynamic>(() => this.LoadAsset("MGNZ.Vendor.Squidex.Client.Tests.Assets.ReferenceMultipleSchema.json")));

    protected ISquidexOAuthClient PlainOAuthClient
    {
      get
      {
        return this._plainOAuthClient ??
               (this._plainOAuthClient =
                 RestService.For<ISquidexOAuthClient>(this.Options.BaseAddressUri.AbsolutePath));
      }
    }

    protected ISquidexAppSchemaClient AuthenticatedSchemaClient
    {
      get
      {
        return this._authenticatedSchemaClient ??
               (this._authenticatedSchemaClient =
                 RestService.For<ISquidexAppSchemaClient>(
                   new HttpClient(new AccessTokenHttpClientHandler(() =>
                     this.GetOAuthValueKnownUser(this.Options.Clients["mgnz-aut-developer"])))
                   {
                     BaseAddress = this.Options.BaseAddressUri
                   }));
      }
    }

    protected ISquidexContentClient AuthenticatedContentClient
    {
      get
      {
        return this._authenticatedContentClient ??
               (this._authenticatedContentClient =
                 RestService.For<ISquidexContentClient>(
                   new HttpClient(new AccessTokenHttpClientHandler(() =>
                     this.GetOAuthValueKnownUser(this.Options.Clients["mgnz-aut-editor"])))
                   {
                     BaseAddress = this.Options.BaseAddressUri
                   }));
      }
    }

    private KnownUserOAuthCreds TempoaryHardCodedTestClientAUT => new KnownUserOAuthCreds
    {
      OAuthAppName = "aut",
      OAuthClientId = "mgnz-aut-testclient",
      OAuthClientSecret = "V3DJ6r8UJgulQIx0nMDWxIhGInBp7bx2DI/QvIkyUnQ="
    };

    private KnownUserOAuthCreds TempoaryHardCodedEditorClientAUT => new KnownUserOAuthCreds
    {
      OAuthAppName = "aut",
      OAuthClientId = "mgnz-aut-testeditor",
      OAuthClientSecret = "rRLoIyQbFo8btOfxiI2kn5LjN7aObqyeSHhnvPiIxoU="
    };

    private async Task<string> GetOAuthValueKnownUser(KnownUserOAuthCreds creds)
    {
      var response = await this.PlainOAuthClient.GetToken(new SquidexGetOAuthTokenRequest
      {
        ClientId = $"{creds.OAuthAppName}:{creds.OAuthClientId}",
        ClientSecret = creds.OAuthClientSecret
      });
      return response.AccessToken;
    }

    protected dynamic LoadAsset(string path) => JsonConvert.DeserializeObject(this.StreamToString(this.GetManifestResourceStream(path)));
    private Stream GetManifestResourceStream(string fullyQualifiedNamespace) => this.GetManifestResourceStream(typeof(SquidexClientIntegrationTestBase).GetTypeInfo().Assembly, fullyQualifiedNamespace);
    private Stream GetManifestResourceStream(Assembly assembly, string fullyQualifiedNamespace) => assembly.GetManifestResourceStream(fullyQualifiedNamespace);

    protected string StreamToString(Stream inputStream)
    {
      using (var reader = new StreamReader(inputStream))
        return reader.ReadToEnd();
    }
  }

  public class ISquidexContentClientIntegrationTests : SquidexClientIntegrationTestBase
  {
    [Fact]
    public async Task EndToEnd_HappyPath()
    {
      JsonConvert.DefaultSettings = () => new JsonSerializerSettings
      {
        DateFormatString = "yyyy-MM-ddTHH:mm:ssZ"
      };

      {
        // assert no schemas on the AUT application
        //var all = await this.AuthenticatedSchemaClient.GetAllSchemas("aut");
        //int count = Convert.ToInt32(all.Count);
        //count.Should().Be(0, "Test cannot start because there are unexpected Schemas on the target endpoint");

        // create schema
        //var referenceSchema = this.Schema1Asset.Value;
        //referenceSchema.name = "schema1name";
        //var created = await this.AuthenticatedSchemaClient.CreateSchema("aut", referenceSchema);
        //var published = await this.AuthenticatedSchemaClient.PublishSchema("aut", "schema1name");
      }

      var record1Expected = ReferenceMultipleAssetModelBuilder.Reference1.Value;
      var record2Expected = ReferenceMultipleAssetModelBuilder.Reference2.Value;

      var record1Id = string.Empty;
      var record2Id = string.Empty;

      // test insert things
      {
        var inserted1Actual = await this.AuthenticatedContentClient.Create("aut", "schema1name", record1Expected);
        record1Id = AssertReference(inserted1Actual, record1Expected, "inserting things");


        var inserted2Actual = await this.AuthenticatedContentClient.Create("aut", "schema1name", record2Expected);
        record2Id = AssertReference(inserted2Actual, record2Expected, "inserting things");
      }

      // publish the things
      {
        await this.AuthenticatedContentClient.Publish("aut", "schema1name", record1Id);
        await this.AuthenticatedContentClient.Publish("aut", "schema1name", record2Id);
      }

      // test get things
      {
        var get1byId = await this.AuthenticatedContentClient.Get<ReferenceMultipleAssetModel>("aut", "schema1name", record1Id);
        AssertReference(get1byId, record1Expected, "getting things");

        var get2byId = await this.AuthenticatedContentClient.Get<ReferenceMultipleAssetModel>("aut", "schema1name", record2Id);
        AssertReference(get2byId, record2Expected, "getting things");
      }

      // test put the things
      {
        var updated1 = ReferenceMultipleAssetModelBuilder.PutReference(record1Expected);
        var update1byId = await this.AuthenticatedContentClient.Put("aut", "schema1name", record1Id, updated1);
        AssertReference(update1byId, record1Expected, "put things");

        //var updated2 = ReferenceMultipleAssetModelBuilder.PutReference(record2Expected);
        //var update2byId = await this.AuthenticatedContentClient.Put("aut", "schema1name", record2Id, updated2);
        //AssertReference(update2byId, record2Expected, "put things");
      }

      // test patch the things
      {
        var patched1expected = new ReferenceMultipleAssetModel { StringField = new SquidexInvariantFieldItem<string>() { Iv = "updated in patch" } };
        var patched1byId = await this.AuthenticatedContentClient.Patch("aut", "schema1name", record1Id, patched1expected);
        patched1byId.StringField.Iv.Should().Be(patched1expected.StringField.Iv, because: $"at stage patch things");

        //var patched2expected = new ReferenceMultipleAssetModel { StringField = new SquidexInvariantFieldItem<string>() { Iv = "updated in patch" } };
        //var patched2byId = await this.AuthenticatedContentClient.Patch("aut", "schema1name", record2Id, patched2expected);
        //patched2byId.StringField.Iv.Should().Be(patched2expected.StringField.Iv, because: $"at stage patch things");
      }

      // query
      {
        var request = SquidexQueryRequestBuilder.Build(requestedFilter: $"data/stringfield/iv eq '{record2Expected.StringField.Iv}'");
        //var query = await this.AuthenticatedContentClient.Query<ReferenceMultipleAssetModel>("aut", "schema1name", request);
        var query = await this.AuthenticatedContentClient.Query<ReferenceMultipleAssetModel>("aut", "schema1name",
          request.Top, request.Skip, request.OrderBy, request.Search, request.Filter);

      }

      // unpublish
      {
        await this.AuthenticatedContentClient.Unpublish("aut", "schema1name", record1Id);
      }

      // delete
      {
        await this.AuthenticatedContentClient.Delete("aut", "schema1name", record1Id);
      }

      // archive
      {
        await this.AuthenticatedContentClient.Archive("aut", "schema1name", record2Id);
      }

      // restore
      {
        await this.AuthenticatedContentClient.Restore("aut", "schema1name", record2Id);
      }

      // query

      {
        // clean up
        //await this.AuthenticatedSchemaClient.DeleteSchema("aut", "schema1name");
        //var all = await this.AuthenticatedSchemaClient.GetAllSchemas("aut");
        //int count = Convert.ToInt32(all.Count);
        //count.Should().Be(0);
      }
    }

    private static void AssertReference(ReferenceMultipleAssetModel inserted1Actual, ReferenceMultipleAssetModel record1Expected, string asserting)
    {
      inserted1Actual.StringField.Iv.Should().Be(record1Expected.StringField.Iv, because: $"at stage {asserting}");
      inserted1Actual.BooleanField.Iv.Should().Be(record1Expected.BooleanField.Iv, because: $"at stage {asserting}");
      inserted1Actual.DateTimeField.Iv.Should().BeCloseTo(record1Expected.DateTimeField.Iv, because: $"at stage {asserting}", precision: TimeSpan.FromSeconds(5));
      inserted1Actual.NumberField.Iv.Should().Be(record1Expected.NumberField.Iv, because: $"at stage {asserting}");
    }

    private static string AssertReference(SquidexItemContent<ReferenceMultipleAssetModel> inserted1Actual, ReferenceMultipleAssetModel record1Expected, string asserting)
    {
      AssertReference(inserted1Actual.Data, record1Expected, asserting);
      inserted1Actual.Id.Should().NotBeNullOrEmpty(because: $"at stage {asserting}").And.NotBeNullOrWhiteSpace(because: $"at stage {asserting}");

      return inserted1Actual.Id;
    }
  }
}
namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.IO;
  using System.Net.Http;
  using System.Reflection;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Tests.Plumbing;
  using MGNZ.Squidex.Client.Tests.Stories;

  using Microsoft.Extensions.Configuration;

  using Newtonsoft.Json;

  using Refit;

  using Xunit;

  [Obsolete]
  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "obsolete-squidex-api-integration")]
  public class SquidexClientIntegrationTest
  {
    private TestConfigurationOptions _options;
    private ISquidexAppSchemaClient _authenticatedSchemaClient;
    private ISquidexContentClient _authenticatedContentClient;
    private ISquidexOAuthClient _plainOAuthClient;
    private Lazy<dynamic> _schemaAsset;

    protected TestConfigurationOptions Options
    {
      get
      {
        if (this._options == null)
        {
          this._options = new TestConfigurationOptions();
          var config = TestHelper.GetConfigurationRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
          config.Bind(this._options);
        }

        return this._options;
      }
    }

    protected Lazy<dynamic> Schema1Asset => this._schemaAsset ?? (this._schemaAsset = new Lazy<dynamic>(() => this.LoadAsset("MGNZ.Squidex.Client.Tests.Assets.ReferenceMultipleSchema.json")));

    protected dynamic LoadAsset(string path) => JsonConvert.DeserializeObject(this.StreamToString(this.GetManifestResourceStream(path)));
    private Stream GetManifestResourceStream(string fullyQualifiedNamespace) => this.GetManifestResourceStream(typeof(SquidexClientIntegrationTest).GetTypeInfo().Assembly, fullyQualifiedNamespace);
    private Stream GetManifestResourceStream(Assembly assembly, string fullyQualifiedNamespace) => assembly.GetManifestResourceStream(fullyQualifiedNamespace);

    protected string StreamToString(Stream inputStream)
    {
      using (var reader = new StreamReader(inputStream))
        return reader.ReadToEnd();
    }
  }
}
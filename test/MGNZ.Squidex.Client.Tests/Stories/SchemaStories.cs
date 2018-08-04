namespace MGNZ.Squidex.Client.Tests.Stories
{
  using System.Net.Http;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.Client.Tests.Plumbing;

  using Refit;

  public class SchemaStories : StoryBase
  {
    private ISquidexAppSchemaClient _authenticatedSchemaClient;

    private readonly OAuthStories _oAuthStories;

    public SchemaStories(TestConfigurationOptions options) : base(options)
    {
      this._oAuthStories = new OAuthStories(options);
    }

    protected ISquidexAppSchemaClient AuthenticatedSchemaClient
    {
      get
      {
        return this._authenticatedSchemaClient ?? (this._authenticatedSchemaClient =
                 RestService.For<ISquidexAppSchemaClient>(
                   new HttpClient(new LazyAccessTokenHttpClientHandler(() =>
                     this._oAuthStories.GetOAuthValueKnownUser(this.Options.Clients["aut-developer"])))
                   {
                     BaseAddress = this.Options.BaseAddressUri
                   }));
      }
    }

    public async Task<dynamic> PostSchema(string app, dynamic schema, string schemaName = null)
    {
      if (!string.IsNullOrEmpty(schemaName) || !string.IsNullOrWhiteSpace(schemaName))
        schema.name = schemaName;

      return await this.AuthenticatedSchemaClient.CreateSchema(app, schema);
    }

    public async Task<dynamic> GetSchemas(string app)
    {
      return await this.AuthenticatedSchemaClient.GetAllSchemas(app);
    }

    public async Task<dynamic> GetSchema(string app, string schema)
    {
      return await this.AuthenticatedSchemaClient.GetSchema(app, schema);
    }

    public async Task<dynamic> PublishSchema(string app, string schema)
    {
      return await this.AuthenticatedSchemaClient.PublishSchema(app, schema);
    }

    public async Task<dynamic> UnpublishSchema(string app, string schema)
    {
      return await this.AuthenticatedSchemaClient.UnpublishSchema(app, schema);
    }

    public async Task<dynamic> DeleteSchema(string app, string schema)
    {
      return await this.AuthenticatedSchemaClient.DeleteSchema(app, schema);
    }

    public async Task<dynamic> UpdateScripts(string app, string schema, object script)
    {
      return await this.AuthenticatedSchemaClient.UpdateScripts(app, schema, script);
    }
  }
}
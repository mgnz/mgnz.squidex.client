namespace MGNZ.Squidex.Client.Tests.Stories
{
  using System.Net.Http;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Tests.Plumbing;
  using MGNZ.Squidex.Client.Transport;

  using Refit;

  public class ContentStories : StoryBase
  {
    private ISquidexContentClient _authenticatedContentClient;

    private readonly OAuthStories _oAuthStories;

    public ContentStories(TestConfigurationOptions options) : base(options)
    {
      this._oAuthStories = new OAuthStories(options);
    }

    protected ISquidexContentClient AuthenticatedContentClient
    {
      get
      {
        return this._authenticatedContentClient ??
               (this._authenticatedContentClient =
                 RestService.For<ISquidexContentClient>(
                   new HttpClient(new AccessTokenHttpClientHandler(() =>
                     this._oAuthStories.GetOAuthValueKnownUser(this.Options.Clients["aut-editor"])))
                   {
                     BaseAddress = this.Options.BaseAddressUri
                   }));
      }
    }


    public async Task<SquidexQueryResponse<TModel>> Query<TModel>(string app, string schema,
      SquidexQueryRequest request)
    {
      return await this.AuthenticatedContentClient.Query<TModel>(app, schema, request);
    }

    public async Task<SquidexQueryResponse<TModel>> Query<TModel>(string app, string schema, int top = 20, int skip = 0,
      string orderBy = null, string search = null,
      string filter = null)
    {
      return await this.AuthenticatedContentClient.Query<TModel>(app, schema, top, skip, orderBy, search, filter);
    }

    public async Task<ItemContent<TModel>> Create<TModel>(string app, string schema, TModel content)
    {
      return await this.AuthenticatedContentClient.Create(app, schema, content);
    }

    public async Task<ItemContent<TModel>> Get<TModel>(string app, string schema, string id)
    {
      return await this.AuthenticatedContentClient.Get<TModel>(app, schema, id);
    }

    public async Task<TModel> Put<TModel>(string app, string schema, string id, TModel content)
    {
      return await this.AuthenticatedContentClient.Put(app, schema, id, content);
    }

    public async Task<TModel> Patch<TModel>(string app, string schema, string id, TModel content)
    {
      return await this.AuthenticatedContentClient.Patch(app, schema, id, content);
    }

    public async Task Publish(string app, string schema, string id)
    {
      await this.AuthenticatedContentClient.Publish(app, schema, id);
    }

    public async Task Unpublish(string app, string schema, string id)
    {
      await this.AuthenticatedContentClient.Unpublish(app, schema, id);
    }

    public async Task Archive(string app, string schema, string id)
    {
      await this.AuthenticatedContentClient.Archive(app, schema, id);
    }

    public async Task Restore(string app, string schema, string id)
    {
      await this.AuthenticatedContentClient.Restore(app, schema, id);
    }

    public async Task Delete(string app, string schema, string id)
    {
      await this.AuthenticatedContentClient.Delete(app, schema, id);
    }
  }
}
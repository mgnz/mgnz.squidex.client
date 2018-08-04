namespace MGNZ.Squidex.Client
{
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Transport;

  using Refit;

  [Headers("Authorization: Bearer", "Cache-Control: no-cache", "Connection: keep-alive",
    "Content-Type: application/json", "Pragma: no-cache", "Accept: application/json")]
  public interface ISquidexContentClient
  {
    [Get("/api/content/{app}/{schema}")]
    Task<QueryResponse<TModel>> Query<TModel>(string app, string schema, [Query] QueryRequest request);

    [Get("/api/content/{app}/{schema}")]
    Task<QueryResponse<TModel>> Query<TModel>(string app, string schema, [AliasAs("$top")] int top = 20,
      [AliasAs("$skip")] int skip = 0, [AliasAs("$orderby")] string orderBy = null,
      [AliasAs("$search")] string search = null, [AliasAs("$filter")] string filter = null);

    [Post("/api/content/{app}/{schema}/")]
    Task<ItemContent<TModel>> Create<TModel>(string app, string schema,
      [Body(BodySerializationMethod.Json)] TModel content);

    [Get("/api/content/{app}/{schema}/{id}/")]
    Task<ItemContent<TModel>> Get<TModel>(string app, string schema, string id);

    [Put("/api/content/{app}/{schema}/{id}")]
    Task<TModel> Put<TModel>(string app, string schema, string id, [Body(BodySerializationMethod.Json)] TModel content);

    [Patch("/api/content/{app}/{schema}/{id}")]
    Task<TModel> Patch<TModel>(string app, string schema, string id,
      [Body(BodySerializationMethod.Json)] TModel content);

    [Put("/api/content/{app}/{schema}/{id}/publish")]
    Task Publish(string app, string schema, string id);

    [Put("/api/content/{app}/{schema}/{id}/unpublish")]
    Task Unpublish(string app, string schema, string id);

    [Put("/api/content/{app}/{schema}/{id}/archive")]
    Task Archive(string app, string schema, string id);

    [Put("/api/content/{app}/{schema}/{id}/restore")]
    Task Restore(string app, string schema, string id);

    [Delete("/api/content/{app}/{schema}/{id}")]
    Task Delete(string app, string schema, string id);
  }
}
namespace MGNZ.Squidex.Client
{
  using System.Threading.Tasks;

  using Refit;

  [Headers("Authorization: Bearer", "Cache-Control: no-cache", "Connection: keep-alive",
    "Content-Type: application/json", "Pragma: no-cache", "Accept: application/json")]
  public interface ISquidexContentClient
  {
    [Get("/api/content/{app}/{schema}")]
    Task<dynamic> QueryContent(string app, string schema, [AliasAs("$top")] int top = 20,
      [AliasAs("$skip")] int skip = 0, [AliasAs("$orderby")] string orderBy = null,
      [AliasAs("$search")] string search = null, [AliasAs("$filter")] string filter = null);

    [Post("/api/content/{app}/{schema}/")]
    Task<dynamic> CreateContent(string app, string schema,
      [Body(BodySerializationMethod.Json)] object content);

    [Get("/api/content/{app}/{schema}/{id}/")]
    Task<dynamic> GetContent(string app, string schema, string id);

    [Put("/api/content/{app}/{schema}/{id}")]
    Task<dynamic> PutContent(string app, string schema, string id, [Body(BodySerializationMethod.Json)] object content);

    [Patch("/api/content/{app}/{schema}/{id}")]
    Task<dynamic> PatchContent(string app, string schema, string id, [Body(BodySerializationMethod.Json)] object content);

    [Put("/api/content/{app}/{schema}/{id}/publish")]
    Task PublishContent(string app, string schema, string id);

    [Put("/api/content/{app}/{schema}/{id}/unpublish")]
    Task UnpublishContent(string app, string schema, string id);

    [Put("/api/content/{app}/{schema}/{id}/archive")]
    Task ArchiveContent(string app, string schema, string id);

    [Put("/api/content/{app}/{schema}/{id}/restore")]
    Task RestoreContent(string app, string schema, string id);

    [Delete("/api/content/{app}/{schema}/{id}")]
    Task DeleteContent(string app, string schema, string id);
  }
}
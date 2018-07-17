namespace MGNZ.Squidex.Client
{
  using System.Threading.Tasks;

  using Refit;

  [Headers("Authorization: Bearer", "Cache-Control: no-cache", "Connection: keep-alive",
    "Content-Type: application/json", "Pragma: no-cache", "Accept: application/json")]
  public interface ISquidexAppSchemaClient
  {
    [Get("/api/apps/{app}/schemas/")]
    Task<dynamic> GetAllSchemas(string app);

    [Get("/api/apps/{app}/schemas/{name}/")]
    Task<dynamic> GetSchema(string app, string name);

    [Post("/api/apps/{app}/schemas/")]
    Task<dynamic> CreateSchema(string app, [Body] object schema);

    [Put("/api/apps/{app}/schemas/{name}/publish/")]
    Task<dynamic> PublishSchema(string app, string name);

    [Put("/api/apps/{app}/schemas/{name}/unpublish/")]
    Task<dynamic> UnpublishSchema(string app, string name);

    [Delete("/api/apps/{app}/schemas/{name}/")]
    Task<dynamic> DeleteSchema(string app, string name);

    [Put("/api/apps/{app}/schemas/{name}/scripts/")]
    Task<dynamic> UpdateScripts(string app, string name, [Body] object scripts);
  }
}
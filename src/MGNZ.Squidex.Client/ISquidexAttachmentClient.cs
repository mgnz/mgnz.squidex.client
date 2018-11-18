namespace MGNZ.Squidex.Client
{
  using System.Net.Http;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Transport;

  using Refit;

  [Headers("Authorization: Bearer", "Cache-Control: no-cache", "Connection: keep-alive",
    "Content-Type: application/json", "Pragma: no-cache", "Accept: application/json")]
  public interface ISquidexAttachmentClient
  {
    [Multipart]
    [Post("/api/apps/{app}/assets/")]
    Task<AttachmentContent> Post(string app, [AliasAs("file")] StreamPart stream);

    [Multipart]
    [Put("/api/apps/{app}/assets/{id}/content")]
    Task<AttachmentContent> Update(string app, string id, [AliasAs("file")] StreamPart stream);

    [Get("/api/apps/{app}/assets/{id}")]
    Task<HttpContent> Get(string app, string id, [Query] AttachmentRequest request);

    [Delete("/api/apps/{app}/assets/{id}")]
    Task<HttpContent> Delete(string app, string id);

    [Put("/api/apps/{app}/assets/{id}")]
    Task Tags(string app, string id, [Body(BodySerializationMethod.Json)] string[ ] tags);

    [Get("/api/content/{app}/{schema}")]
    Task<ListResponse<AttachmentContent>> List(string app, string schema, [Query] ListRequest request);
  }
}
namespace MGNZ.Squidex.Client
{
  using System.Collections.Generic;
  using System.IO;
  using System.Net.Http;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Transport;

  using Refit;

  [Headers("Authorization: Bearer", "Cache-Control: no-cache", "Connection: keep-alive","Pragma: no-cache")]
  public interface ISquidexAttachmentClient
  {
    [Multipart]
    [Post("/api/apps/{app}/assets")]
    Task<AttachmentContent> Post(string app, [AliasAs("file")] IEnumerable<StreamPart> streams);

    [Multipart]
    [Put("/api/apps/{app}/assets/{id}/content")]
    Task<AttachmentContent> Update(string app, string id, [AliasAs("file")] IEnumerable<StreamPart> streams);

    [Get("/api/apps/{app}/assets/{id}/")]
    Task<AttachmentContent> Get(string app, string id);

    [Delete("/api/apps/{app}/assets/{id}")]
    Task<HttpContent> Delete(string app, string id);

    [Put("/api/apps/{app}/assets/{id}/")]
    Task UpdateTags(string app, string id, [Body(BodySerializationMethod.Json)] UpdateAssetDto request);

    [Get("/api/apps/{app}/assets")]
    Task<ListResponse<AttachmentContent>> List(string app, [Query] ListRequest request);
  }
}
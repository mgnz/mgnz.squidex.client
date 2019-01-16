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
    Task<AttachmentContent> CreateAsset(string app, [AliasAs("file")] IEnumerable<StreamPart> streams);

    [Multipart]
    [Put("/api/apps/{app}/assets/{id}/content")]
    Task<AttachmentContent> UpdateAsset(string app, string id, [AliasAs("file")] IEnumerable<StreamPart> streams);

    [Get("/api/apps/{app}/assets/{id}/")]
    Task<AttachmentContent> GetAsset(string app, string id);

    //[Get("/api/apps/{app}/assets/")]
    //Task<AttachmentContent> GetAssets(string app, [Body(BodySerializationMethod.Json)] string ids);

    [Delete("/api/apps/{app}/assets/{id}")]
    Task<HttpContent> DeleteAsset(string app, string id);

    [Put("/api/apps/{app}/assets/{id}/")]
    Task UpdateAssetTags(string app, string id, [Body(BodySerializationMethod.Json)] UpdateAssetDto request);

    [Get("/api/apps/{app}/assets/tags")]
    Task<Dictionary<string, int>> GetAllAssetTags(string app);

    [Get("/api/apps/{app}/assets")]
    Task<ListResponse<AttachmentContent>> GetAllAssets(string app, [Query] ListRequest request);
  }
}
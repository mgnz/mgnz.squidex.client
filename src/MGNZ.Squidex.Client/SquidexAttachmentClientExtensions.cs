namespace MGNZ.Squidex.Client
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Model;

  using Refit;

  public static class SquidexAttachmentClientExtensions
  {
    public static async Task<AttachmentContent> CreateAsset(this ISquidexAttachmentClient that, string application, string fileName, string mimeType, Stream stream)
    {
      return await that.CreateAsset(application, new[]
      {
        new StreamPart(stream, fileName, mimeType)
      });
    }

    public static async Task<AttachmentContent> UpdateAssetById(this ISquidexAttachmentClient that, string application, string id, string fileName, string mimeType, Stream stream)
    {
      return await that.UpdateAsset(application, id, new[]
      {
        new StreamPart(stream, fileName, mimeType)
      });
    }

    public static async Task<AttachmentContent> UpdateAssetContentByName(this ISquidexAttachmentClient that, string application, string fileName, string mimeType, Stream stream)
    {
      var item = await that.GetAssetByNameOrDefault(application, fileName);

      return await that.UpdateAssetById(application, item.Id, fileName, mimeType, stream);
    }

    public static async Task DeleteAssetByName(this ISquidexAttachmentClient that, string application, string fileName)
    {
      var item = await that.GetAssetByNameOrDefault(application, fileName);

      await that.DeleteAsset(application, item.Id);
    }

    public static async Task<AttachmentContent> GetAssetByNameOrDefault(this ISquidexAttachmentClient that, string application, string fileName)
    {
      // todo : pagination, caching ??

      var data = await that.GetAllAssets(application, new ListRequest()
      {
        Skip = 0, Top = 200
      });

      var count = data.Total;
      if (count == 0) return default(AttachmentContent);

      var item = data.Items.SingleOrDefault(d => CheckEquality(d, fileName));

      return item;
    }

    public static async Task<bool> AttachmentExists(this ISquidexAttachmentClient that, string application,
      string fileName = null)
    {
      var item = await that.GetAssetByNameOrDefault(application, fileName);

      return item != null;
    }

    private static bool CheckEquality(AttachmentContent d, string name)
    {
      var dynamicName = Convert.ToString(d.FileName);

      return name.Equals(dynamicName);
    }
  }
}
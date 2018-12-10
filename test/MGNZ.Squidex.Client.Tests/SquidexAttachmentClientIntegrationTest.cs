namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Tests.Shared.Assets;
  using MGNZ.Squidex.Client.Tests.Shared.Code;
  using MGNZ.Squidex.Client.Transport;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-api-integration")]
  public class SquidexAttachmentClientIntegrationTest : BaseHandlerIntegrationTest
  {
    [Fact()]
    public async Task PostAsset_Execute_EndToEnd()
    {
      var attachmentName = $"{base.GetRandomSchemaName}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut");

      var createResponse = await AttachmentClient.Post("aut", attachmentName, "image/jpeg", AssetLoader.Asset2);
      await AttachmentClient.AssertAttachmentMustExist("aut", attachmentName, delay: TimeSpan.FromSeconds(2));
      // todo : assert the postresponse matches 

      var id = createResponse.Id;
      var deleteResponse = await AttachmentClient.DeleteAsset("aut", id);

      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task UpdateAssetContent_Execute_EndToEnd()
    {
      var attachmentName = $"{base.GetRandomSchemaName}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut");
      var createResponse = await AttachmentClient.Post("aut", attachmentName, "image/jpeg", AssetLoader.Asset2);
      await AttachmentClient.AssertAttachmentMustExist("aut", attachmentName, delay: TimeSpan.FromSeconds(2));

      var id = createResponse.Id;

      var updateResponse = await AttachmentClient.Update("aut", id, attachmentName, "image/jpeg", AssetLoader.Asset3);
      // todo : assert the putresponse matches 

      var deleteResponse = await AttachmentClient.DeleteAsset("aut", id);
      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task UpdateAssetTags_Execute_EndToEnd()
    {
      var attachmentName = $"{base.GetRandomSchemaName}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut");
      var createResponse = await AttachmentClient.Post("aut", attachmentName, "image/jpeg", AssetLoader.Asset2);
      await AttachmentClient.AssertAttachmentMustExist("aut", attachmentName, delay: TimeSpan.FromSeconds(2));

      var id = createResponse.Id;
      var fileName = createResponse.FileName;
      var existingTags = createResponse.Tags;
      var modifiedTags = existingTags.Append("new-tag").Append("another-new-tag").ToArray();

      await AttachmentClient.UpdateAssetTags("aut", id, new UpdateAssetDto()
      {
        FileName = fileName,
        Tags = modifiedTags
      });

      var updatedAttachment = await AttachmentClient.GetAsset("aut", id);
      // todo : assert the updatedresponse matches 

      var deleteResponse = await AttachmentClient.DeleteAsset("aut", id);
      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task GetAsset_Execute_EndToEnd()
    {
      var attachmentName = $"{base.GetRandomSchemaName}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut");
      var createResponse = await AttachmentClient.Post("aut", attachmentName, "image/jpeg", AssetLoader.Asset2);
      await AttachmentClient.AssertAttachmentMustExist("aut", attachmentName, delay: TimeSpan.FromSeconds(2));

      var id = createResponse.Id;
      var getAssetResponse = await AttachmentClient.GetAsset("aut", id);
      // todo : assert the updatedresponse matches 

      var deleteResponse = await AttachmentClient.DeleteAsset("aut", id);
      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task DeleteAsset_Execute_EndToEnd()
    {
      var attachmentName = $"{base.GetRandomSchemaName}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut");
      var createResponse = await AttachmentClient.Post("aut", attachmentName, "image/jpeg", AssetLoader.Asset2);
      await AttachmentClient.AssertAttachmentMustExist("aut", attachmentName, delay: TimeSpan.FromSeconds(2));

      var id = createResponse.Id;
      var deleteResponse = await AttachmentClient.DeleteAsset("aut", id);
      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(2));
    }

    [Fact(Skip = "in progress")]
    public async Task GetAllTags_Execute_EndToEnd()
    {
    }

    [Fact]
    public async Task GetAssets_Execute_EndToEnd()
    {
      var attachmen1tName = $"{base.GetRandomSchemaName}.jpg";
      var attachment2Name = $"{base.GetRandomSchemaName}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut");

      var createResponse1 = await AttachmentClient.Post("aut", attachmen1tName, "image/jpeg", AssetLoader.Asset2);
      await AttachmentClient.AssertAttachmentMustExist("aut", attachmen1tName, delay: TimeSpan.FromSeconds(2));
      var createResponse2 = await AttachmentClient.Post("aut", attachment2Name, "image/jpeg", AssetLoader.Asset3);
      await AttachmentClient.AssertAttachmentMustExist("aut", attachment2Name, delay: TimeSpan.FromSeconds(2));

      var getAllResponse = await AttachmentClient.GetAssets("aut", new ListRequest()
      {
        Skip = 0,
        Top = 2,
      });

      // todo : assert we got both

      var id1 = createResponse1.Id;
      var id2 = createResponse2.Id;
      var deleteResponse1 = await AttachmentClient.DeleteAsset("aut", id1);
      var deleteResponse2 = await AttachmentClient.DeleteAsset("aut", id2);

      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(2));
    }
  }
}

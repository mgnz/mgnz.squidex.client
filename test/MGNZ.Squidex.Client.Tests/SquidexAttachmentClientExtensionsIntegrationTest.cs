namespace MGNZ.Squidex.Client.Tests
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;
  using FluentAssertions;
  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Tests.Shared.Assets;
  using MGNZ.Squidex.Client.Tests.Shared.Code;
  using MGNZ.Squidex.Client.Transport;
  using Refit;
  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-api-integration")]
  public class SquidexAttachmentClientExtensionsIntegrationTest : BaseHandlerIntegrationTest
  {
    [Fact()]
    public async Task Post_Execute()
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

    [Fact()]
    public async Task UpdateAssetContentById_Execute()
    {
      var attachmentName = $"{base.GetRandomSchemaName}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut");
      var createResponse = await AttachmentClient.Post("aut", attachmentName, "image/jpeg", AssetLoader.Asset2);
      await AttachmentClient.AssertAttachmentMustExist("aut", attachmentName, delay: TimeSpan.FromSeconds(2));

      var id = createResponse.Id;

      var updateResponse = await AttachmentClient.UpdateAssetContentById("aut", id, attachmentName, "image/jpeg", AssetLoader.Asset3);
      // todo : assert the putresponse matches 

      var deleteResponse = await AttachmentClient.DeleteAsset("aut", id);
      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(2));
    }

    [Fact()]
    public async Task UpdateAssetContentByName_Execute()
    {
      var attachmentName = $"{base.GetRandomSchemaName}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut");
      var createResponse = await AttachmentClient.Post("aut", attachmentName, "image/jpeg", AssetLoader.Asset2);
      await AttachmentClient.AssertAttachmentMustExist("aut", attachmentName, delay: TimeSpan.FromSeconds(2));

      var id = createResponse.Id;

      var updateResponse = await AttachmentClient.UpdateAssetContentByName("aut", attachmentName, "image/jpeg", AssetLoader.Asset3);
      // todo : assert the putresponse matches 

      var deleteResponse = await AttachmentClient.DeleteAsset("aut", id);
      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(2));
    }

    [Fact()]
    public async Task DeleteByName_Execute()
    {
      var attachmentName = $"{base.GetRandomSchemaName}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut");
      var createResponse = await AttachmentClient.Post("aut", attachmentName, "image/jpeg", AssetLoader.Asset2);
      await AttachmentClient.AssertAttachmentMustExist("aut", attachmentName, delay: TimeSpan.FromSeconds(2));

      var id = createResponse.Id;

      await AttachmentClient.DeleteByName("aut", attachmentName);
      // todo : assert the putresponse matches 

      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(2));
    }

    [Fact()]
    public async Task AttachmentExists_Execute()
    {
      var attachmentName = $"{base.GetRandomSchemaName}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut");

      var createResponse = await AttachmentClient.PostAsset("aut", new[]
      {
        new StreamPart(AssetLoader.Asset2, attachmentName, "image/jpeg")
      });

      var exists = await AttachmentClient.AttachmentExists("aut", attachmentName);
      exists.Should().BeTrue();

      var id = createResponse.Id;
      var deleteResponse = await AttachmentClient.DeleteAsset("aut", id);

      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(2));
    }
  }
}

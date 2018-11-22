namespace MGNZ.Squidex.Client.Tests.Shared.Code
{
  using System;
  using System.Threading.Tasks;

  using FluentAssertions;

  internal static class SquidexAttachmentClientAssertExtensions
  {
    public static async Task AssertNoAttachmentsExist(this ISquidexAttachmentClient that, string application, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = await that.AttachmentExists(application);
      exists.Should().BeFalse();
    }

    public static async Task AssertAttachmentMustExist(this ISquidexAttachmentClient that, string application, string name, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = await that.AttachmentExists(application, name);
      exists.Should().BeTrue();
    }

    public static async Task AssertAttachmentMustNotExist(this ISquidexAttachmentClient that, string application, string name, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = await that.AttachmentExists(application, name);
      exists.Should().BeFalse();
    }
  }
}
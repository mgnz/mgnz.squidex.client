namespace MGNZ.Squidex.Client.Tests.Shared.Code
{
  using System;
  using System.Threading.Tasks;

  using FluentAssertions;

  internal static class SquidexContentClientAssertExtensions
  {
    public static async Task AssertContentMustExists(this ISquidexContentClient that, string application, string schema, string id, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = false;

      try
      {
        await that.Get<dynamic>(application, schema, id);
        exists = true;
      }
      catch (Exception e)
      {
        exists = false;
      }

      exists.Should().BeTrue();
    }

    public static async Task AssertContentMustNotExists(this ISquidexContentClient that, string application, string schema, string id, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = false;

      try
      {
        await that.Get<dynamic>(application, schema, id);
        exists = true;
      }
      catch (Exception e)
      {
        exists = false;
      }

      exists.Should().BeFalse();
    }
  }
}
namespace MGNZ.Squidex.Tests.Shared.Code
{
  using System;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client;

  internal static class SquidexAppSchemaClientAssertExtensions
  {
    public static async Task AssertNoSchemasExist(this ISquidexAppSchemaClient that, string application, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = await that.SchemaExists(application);
      exists.Should().BeFalse();
    }

    public static async Task AssertSchemaMustExist(this ISquidexAppSchemaClient that, string application, string name, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = await that.SchemaExists(application, name);
      exists.Should().BeTrue();
    }

    public static async Task AssertSchemaMustNotExist(this ISquidexAppSchemaClient that, string application, string name, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = await that.SchemaExists(application, name);
      exists.Should().BeFalse();
    }
  }
}
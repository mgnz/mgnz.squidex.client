namespace MGNZ.Squidex.Client
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  public static class SquidexAppSchemaClientExtensions
  {
    public static async Task<bool> SchemaExists(this ISquidexAppSchemaClient that, string application,
      string name = null)
    {
      var data = await that.GetAllSchemas(application);

      var count = Convert.ToInt32(data.Count);
      if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
      {
        return count > 0;
      }

      var any = ((IEnumerable<dynamic>) data).Any(d => CheckEquality(d, name));
      return count != 0 && any;
    }

    private static bool CheckEquality(dynamic d, string name)
    {
      var dynamicName = Convert.ToString(d.name);

      return name.Equals(dynamicName);
    }
  }
}
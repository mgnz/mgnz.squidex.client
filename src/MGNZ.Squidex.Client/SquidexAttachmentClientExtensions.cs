namespace MGNZ.Squidex.Client
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Model;

  public static class SquidexAttachmentClientExtensions
  {
    public static async Task<bool> AttachmentExists(this ISquidexAttachmentClient that, string application,
      string name = null)
    {
      // todo : pagination
      var data = await that.List(application, new ListRequest()
      {
        Skip = 0, Top = 200
      });

      var count = data.Total;
      if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
      {
        return count > 0;
      }

      var any = data.Items.Any(d => CheckEquality(d, name));
      return count != 0 && any;
    }

    private static bool CheckEquality(AttachmentContent d, string name)
    {
      var dynamicName = Convert.ToString(d.FileName);

      return name.Equals(dynamicName);
    }
  }
}
namespace MGNZ.Squidex.Client.Tests.Plumbing
{
  using Microsoft.Extensions.Configuration;


  public class ConfigurationAwareTestBase
  {
    protected IConfigurationRoot GetConfigurationRoot(string outputPath = null)
    {
      var builder = new ConfigurationBuilder();
      builder.AddEnvironmentVariables();

      if (outputPath != null)
      {
        builder.SetBasePath(outputPath);
        builder.AddJsonFile("appsettings.json", optional: true);
      }

      return builder.Build();
    }
  }
}
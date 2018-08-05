namespace MGNZ.Squidex.Client.Tests.Plumbing
{
  using Microsoft.Extensions.Configuration;

  public static class TestHelper
  {
    public static IConfigurationRoot GetConfigurationRoot(string outputPath = null, string userSecrets = null)
    {
      var builder = new ConfigurationBuilder();
      builder.AddEnvironmentVariables();

      if (outputPath != null)
      {
        builder.SetBasePath(outputPath);
        builder.AddJsonFile("testsettings.json", true);
        builder.AddJsonFile("testsettings.aut.json", true);
      }

      //if (userSecrets != null)
      //{
      //  builder.AddUserSecrets("e3dfcccf-0cb3-423a-b302-e3e92e95c128")
      //}

      return builder.Build();
    }
  }
}
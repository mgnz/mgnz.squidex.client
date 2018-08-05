namespace MGNZ.Squidex.Client.Tests.Plumbing
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Reflection;

  using FluentAssertions;

  using MGNZ.Squidex.Client.Tests.Stories;

  using Microsoft.Extensions.Configuration;

  using Newtonsoft.Json;

  using Xunit;

  [Trait("category", "unit")]
  public class VerifyPlumbing
  {
    [Fact(Skip = "only run manually")]
    public void Plumbing_GenerateConfigFromCode()
    {
      var options = new TestConfigurationOptions
      {
        BaseAddressUri = new Uri("http://playpen.cms.a.b.c:80"),
        AdministratorToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjkxRkRENEVCRDYwNjMxNURFRE1234567890MERFRkZBMjFEREE2NkIiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJrZjNVNjlZR01WM3R1QXpRa3czdi1pSGRwbXMifQ",
        Clients = new Dictionary<string, KnownUserOAuthCreds>
        {
          {
            "aut-developer",
            new KnownUserOAuthCreds
            {
              OAuthAppName = "aut",
              OAuthClientId = "aut-testclient",
              OAuthClientSecret = "V3DJ6r8UJgulQIx1234567890nBp7bx2DI/QvIkyUnQ="
            }
          },
          {
            "aut-editor",
            new KnownUserOAuthCreds
            {
              OAuthAppName = "aut",
              OAuthClientId = "aut-testeditor",
              OAuthClientSecret = "rRLoIyQbFo8btOfxi1234567890ObqyeSHhnvPiIxoU="
            }
          }
        }
      };

      var s = JsonConvert.SerializeObject(options);

      s.Should().NotBeNullOrWhiteSpace();
      s.Should().NotBeNullOrEmpty();
    }

    [Fact(Skip = "only run manually")]
    public void Plumbing_SuccessfullyLoadsConfiguration()
    {
      var options = new TestConfigurationOptions();
      var config = TestHelper.GetConfigurationRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
      config.Bind(options);
    }
  }
}
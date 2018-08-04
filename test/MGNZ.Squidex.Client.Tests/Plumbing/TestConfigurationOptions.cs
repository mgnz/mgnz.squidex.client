namespace MGNZ.Squidex.Client.Tests.Plumbing
{
  using System;
  using System.Collections.Generic;

  using MGNZ.Squidex.Client.Tests.Stories;

  public class TestConfigurationOptions
  {
    public Uri BaseAddressUri { get; set; }
    public string AdministratorToken { get; set; }
    public Dictionary<string, KnownUserOAuthCreds> Clients { get; set; }
  }
}
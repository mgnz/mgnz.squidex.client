using System;
using System.Collections.Generic;
using System.Text;

namespace MGNZ.Squidex.Client.Tests.Plumbing
{
  using MGNZ.Squidex.Client.Tests.Stories;

  public class TestConfigurationOptions
    {
      public Uri BaseAddressUri { get; set; }
      public Dictionary<string, KnownUserOAuthCreds> Clients { get; set; }
    }
}

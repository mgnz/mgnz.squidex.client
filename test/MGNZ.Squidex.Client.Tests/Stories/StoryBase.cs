namespace MGNZ.Squidex.Client.Tests.Stories
{
  using System;

  using MGNZ.Squidex.Client.Tests.Plumbing;

  public class StoryBase
  {
    public StoryBase(TestConfigurationOptions options)
    {
      this.Options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public TestConfigurationOptions Options { get; }
  }
}
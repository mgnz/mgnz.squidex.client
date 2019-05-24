#if has_typed_client

using Xunit;

using FluentAssertions;
using Newtonsoft.Json;
using MGNZ.Squidex.Tests.Shared.Assets;
using MGNZ.Squidex.Client.Tests._Serialization._Model;

namespace MGNZ.Squidex.Client.Tests._Serialization
{
  public class ReferenceModelSerializationTests
  {
    [Fact]
    public void SerializedModelMatchesExpectedModel()
    {
      var referenceJson = AssetLoader.AsString(AssetLoader.UnitTestsSerialization1);

      var model = JsonConvert.DeserializeObject<ReferenceMultipleAssetModel>(referenceJson);
      model.Should().NotBeNull();

      var serialized = JsonConvert.SerializeObject(model);
      serialized.Should().NotBeNull();

    }
  }
}

#endif

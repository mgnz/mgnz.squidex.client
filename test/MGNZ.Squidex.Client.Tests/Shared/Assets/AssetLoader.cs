namespace MGNZ.Squidex.Client.Tests.Shared.Assets
{
  using System.IO;
  using System.Reflection;

  using Newtonsoft.Json;

  public partial class AssetLoader
  {
    public static string ExecutingPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static string AssetPath => Path.Combine(ExecutingPath, "Shared", "Assets");

    public static Stream Asset1 => LoadBinaryAsset($"{ns}.app1.asset1.7z");
    public static dynamic Asset1PostResponse => LoadAssetDeserialize<dynamic>($"{ns}.app1.asset1.7z.post.response.json");
    public static dynamic Asset1TagResponse => LoadAssetDeserialize<dynamic>($"{ns}.app1.asset1.7z.tag.response.json");
    public static Stream Asset2 => LoadBinaryAsset($"{ns}.app1.asset2.jpg");
    public static dynamic Asset2PostResponse => LoadAssetDeserialize<dynamic>($"{ns}.app1.asset2.jpg.post.response.json");
    public static dynamic Asset2TagResponse => LoadAssetDeserialize<dynamic>($"{ns}.app1.asset2.jpg.tag.response.json");
    public static Stream Asset3 => LoadBinaryAsset($"{ns}.app1.asset3.jpg");
    public static dynamic Asset3PostResponse => LoadAssetDeserialize<dynamic>($"{ns}.app1.asset3.jpg.post.response.json");
    public static dynamic Asset3TagResponse => LoadAssetDeserialize<dynamic>($"{ns}.app1.asset3.jpg.tag.response.json");

    public static dynamic Schema1() => LoadAssetDeserialize<dynamic>($"{ns}.schema1.json");
    public static dynamic Schema1(string name)
    {
      // note : because my asset data does not store a reference to a name we add it on the fly.

      var schema = Schema1();
      schema.name = name;
      return schema;
    }

    public static dynamic Schema1Data1Post => LoadAssetDeserialize<dynamic>($"{ns}.schema1.data.1.post.json");
    public static dynamic Schema1Data1PostResponse => LoadAssetDeserialize<dynamic>($"{ns}.schema1.data.1.post.response.json");
    public static dynamic Schema1Data2Post => LoadAssetDeserialize<dynamic>($"{ns}.schema1.data.2.post.json");
    public static dynamic Schema1Data2PostResponse => LoadAssetDeserialize<dynamic>($"{ns}.schema1.data.2.post.response.json");
    public static dynamic Schema1DataImport => LoadAssetDeserialize<dynamic>($"{ns}.schema1.data.import.json");
    public static dynamic Schema1DataImportResponse => LoadAssetDeserialize<dynamic>($"{ns}.schema1.data.import.response.json");
    public static dynamic Schema1DataQueryResponse => LoadAssetDeserialize<dynamic>($"{ns}.schema1.data.query.response.json");
    public static dynamic Schema1DataExportResponse => LoadAssetDeserialize<dynamic>($"{ns}.schema1.data.export.response.json");

    public static object Schema1Data1PostDeserialized => LoadAssetDeserialize<object>($"{ns}.schema1.data.1.post.json");
    public static object Schema1Data1PostResponseDeserialized => LoadAssetDeserialize<object>($"{ns}.schema1.data.1.post.response.json");
    public static object Schema1Data2PostDeserialized => LoadAssetDeserialize<object>($"{ns}.schema1.data.2.post.json");
    public static object Schema1Data2PostResponseDeserialized => LoadAssetDeserialize<object>($"{ns}.schema1.data.2.post.response.json");
    public static object Schema1DataImportDeserialized => LoadAssetDeserialize<object>($"{ns}.schema1.data.import.json");
    public static object Schema1DataImportResponseDeserialized => LoadAssetDeserialize<object>($"{ns}.schema1.data.import.response.json");
    public static object Schema1DataQueryResponseDeserialized => LoadAssetDeserialize<object>($"{ns}.schema1.data.query.response.json");
    public static object Schema1DataExportResponseDeserialized => LoadAssetDeserialize<object>($"{ns}.schema1.data.export.response.json");

    public static string Asset1Path => Path.Combine(AssetPath, "app1.asset1.7z");
    public static string Asset1PostResponsePath => Path.Combine(AssetPath, "app1.asset1.7z.post.response.json");
    public static string Asset1TagResponsePath => Path.Combine(AssetPath, "app1.asset1.7z.tag.response.json");
    public static string Asset2Path => Path.Combine(AssetPath, "app1.asset2.jpg");
    public static string Asset2PostResponsePath => Path.Combine(AssetPath, "app1.asset2.jpg.post.response.json");
    public static string Asset2TagResponsePath => Path.Combine(AssetPath, "app1.asset2.jpg.tag.response.json");
    public static string Asset3Path => Path.Combine(AssetPath, "app1.asset3.jpg");
    public static string Asset3PostResponsePath => Path.Combine(AssetPath, "app1.asset3.jpg.post.response.json");
    public static string Asset3TagResponsePath => Path.Combine(AssetPath, "app1.asset3.jpg.tag.response.json");

    public static string Schema1Path => Path.Combine(AssetPath, "schema1.json");
    public static string Schema1Data1PostPath => Path.Combine(AssetPath, "schema1.data.1.post.json");
    public static string Schema1Data1PostResponsePath => Path.Combine(AssetPath, "schema1.data.1.post.response.json");
    public static string Schema1Data2PostPath => Path.Combine(AssetPath, "schema1.data.2.post.json");
    public static string Schema1Data2PostResponsePath => Path.Combine(AssetPath, "schema1.data.2.post.response.json");
    public static string Schema1DataImportPath => Path.Combine(AssetPath, "schema1.data.import.json");
    public static string Schema1DataImportResponsePath => Path.Combine(AssetPath, "schema1.data.import.response.json");
    public static string Schema1DataExportResponsePath => Path.Combine(AssetPath, "schema1.data.export.response.json");

    public static string ExportPath => Path.Combine(ExecutingPath, "Exports");

    public static T LoadAssetDeserialize<T>(string path) => JsonConvert.DeserializeObject<T>(StreamToString(GetManifestResourceStream(path)));

    public static dynamic LoadBinaryAsset(string path) => GetManifestResourceStream(path);

    private static Stream GetManifestResourceStream(string fullyQualifiedNamespace) => GetManifestResourceStream(typeof(AssetLoader).GetTypeInfo().Assembly, fullyQualifiedNamespace);
    private static Stream GetManifestResourceStream(Assembly assembly, string fullyQualifiedNamespace) => assembly.GetManifestResourceStream(fullyQualifiedNamespace);

    protected static string StreamToString(Stream inputStream)
    {
      using (var reader = new StreamReader(inputStream))
        return reader.ReadToEnd();
    }
  }
}
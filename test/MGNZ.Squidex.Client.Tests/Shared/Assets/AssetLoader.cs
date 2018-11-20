namespace MGNZ.Squidex.Client.Tests.Shared.Assets
{
  using System;
  using System.IO;
  using System.Reflection;

  using Newtonsoft.Json;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-api-integration")]
  public class AssetLoader
  {
    public static string ExecutingPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static string AssetPath => Path.Combine(ExecutingPath, "Assets");
    private static string ns => typeof(AssetLoader).Namespace;

    public static Lazy<Stream> Asset1 => new Lazy<Stream>(LoadBinaryAsset($"{ns}.app1.asset1.7z"));
    public static Lazy<dynamic> Asset1PostResponse => new Lazy<dynamic>(LoadAsset($"{ns}.app1.asset1.7z.post.response.json"));
    public static Lazy<dynamic> Asset1TagResponse => new Lazy<dynamic>(LoadAsset($"{ns}.app1.asset1.7z.tag.response.json"));
    public static Lazy<Stream> Asset2 => new Lazy<Stream>(LoadBinaryAsset($"{ns}.app1.asset2.jpg"));
    public static Lazy<dynamic> Asset2PostResponse => new Lazy<dynamic>(LoadAsset($"{ns}.app1.asset2.jpg.post.response.json"));
    public static Lazy<dynamic> Asset2TagResponse => new Lazy<dynamic>(LoadAsset($"{ns}.app1.asset2.jpg.tag.response.json"));

    public static Lazy<dynamic> Schema1 => LoadAsset($"{ns}.schema1.json");
    public static Lazy<dynamic> Schema1Data1Post => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.1.post.json"));
    public static Lazy<dynamic> Schema1Data1PostResponse => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.1.post.response.json"));
    public static Lazy<dynamic> Schema1Data2Post => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.2.post.json"));
    public static Lazy<dynamic> Schema1Data2PostResponse => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.2.post.response.json"));
    public static Lazy<dynamic> Schema1DataImport => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.import.json"));
    public static Lazy<dynamic> Schema1DataImportResponse => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.import.response.json"));
    public static Lazy<dynamic> Schema1DataQueryResponse => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.query.response.json"));
    public static Lazy<dynamic> Schema1DataExportResponse => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.export.response.json"));


    public static string Asset1Path => Path.Combine(AssetPath, "app1.asset1.7z");
    public static string Asset1PostResponsePath => Path.Combine(AssetPath, "app1.asset1.7z.post.response.json");
    public static string Asset1TagResponsePath => Path.Combine(AssetPath, "app1.asset1.7z.tag.response.json");
    public static string Asset2Path => Path.Combine(AssetPath, "app1.asset2.jpg");
    public static string Asset2PostResponsePath => Path.Combine(AssetPath, "app1.asset2.jpg.post.response.json");
    public static string Asset2TagResponsePath => Path.Combine(AssetPath, "app1.asset2.jpg.tag.response.json");

    public static string Schema1Path => Path.Combine(AssetPath, "schema1.json");
    public static string Schema1Data1PostPath => Path.Combine(AssetPath, "schema1.data.1.post.json");
    public static string Schema1Data1PostResponsePath => Path.Combine(AssetPath, "schema1.data.1.post.response.json");
    public static string Schema1Data2PostPath => Path.Combine(AssetPath, "schema1.data.2.post.json");
    public static string Schema1Data2PostResponsePath => Path.Combine(AssetPath, "schema1.data.2.post.response.json");
    public static string Schema1DataImportPath => Path.Combine(AssetPath, "schema1.data.import.json");
    public static string Schema1DataImportResponsePath => Path.Combine(AssetPath, "schema1.data.import.response.json");
    public static string Schema1DataExportResponsePath => Path.Combine(AssetPath, "schema1.data.export.response.json");

    public static string ExportPath => Path.Combine(ExecutingPath, "Exports");

    public static dynamic LoadAsset(string path) => JsonConvert.DeserializeObject<dynamic>(StreamToString(GetManifestResourceStream(path)));
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
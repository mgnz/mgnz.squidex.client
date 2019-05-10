namespace MGNZ.Squidex.Tests.Shared.Assets
{
  using System;
  using System.IO;
  using System.Reflection;

  using Newtonsoft.Json;

  public partial class AssetLoader
  {
    public static string ExecutingPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static string AssetPath => Path.Combine(ExecutingPath, "Shared", "Assets");

    public static dynamic AsDynamic(string file) => LoadFileDeserialize<dynamic>(AsPath(file));
    public static dynamic AsDeserialize(string file) => LoadFileDeserialize<dynamic>(AsPath(file));
    public static string AsPath(string file) => Path.Combine(AssetPath, file);
    public static Stream AsStream(string file) => LoadFileStream(AsPath(file));

    public static string Schema1Name => "schema1.json";

    public static string Schema1Data1PostName => "schema1.data.1.post.json";
    public static string Schema1Data1PostResponseName => "schema1.data.1.post.response.json";
    public static string Schema1Data2PostName => "schema1.data.2.post.json";
    public static string Schema1Data2PostResponseName => "schema1.data.2.post.response.json";
    public static string Schema1DataImportName => "schema1.data.import.json";
    public static string Schema1DataImportResponseName => "schema1.data.import.response.json";
    public static string Schema1DataQueryResponseName => "schema1.data.query.response.json";
    public static string Schema1DataExportResponseName => "schema1.data.export.response.json";

    public static string App1Asset1Name => "app1.asset1.7z";
    public static string App1Asset1PostResponseName => "app1.asset1.7z.post.response.json";
    public static string App1Asset1TagResponseName => "app1.asset1.7z.tag.response.json";
    public static string App1Asset2Name => "app1.asset2.jpg";
    public static string App1Asset2PostResponseName => "app1.asset2.jpg.post.response.json";
    public static string App1Asset2TagResponseName => "app1.asset2.jpg.tag.response.json";
    public static string App1Asset3Name => "app1.asset3.jpg";
    public static string App1Asset3PostResponseName => "app1.asset3.jpg.post.response.json";
    public static string App1Asset3TagResponseName => "app1.asset3.jpg.tag.response.json";

    public static dynamic Schema1(string name)
    {
      // note : because my asset data does not store a reference to a name we add it on the fly.

      var schema = AsDynamic(Schema1Name);
      schema.name = name;
      return schema;
    }

    public static string ExportPath => Path.Combine(ExecutingPath, "Exports");

    public static T LoadFileDeserialize<T>(string path) => JsonConvert.DeserializeObject<T>(StreamToString(LoadFileStream(path)));
    public static Stream LoadFileStream(string path) => File.OpenRead(path);

    public static T LoadResourceDeserialize<T>(string path) => JsonConvert.DeserializeObject<T>(StreamToString(GetManifestResourceStream(path)));
    public static Stream LoadResourceStream(string path) => GetManifestResourceStream(path);
    private static Stream GetManifestResourceStream(string fullyQualifiedNamespace) => GetManifestResourceStream(typeof(AssetLoader).GetTypeInfo().Assembly, fullyQualifiedNamespace);
    private static Stream GetManifestResourceStream(Assembly assembly, string fullyQualifiedNamespace) => assembly.GetManifestResourceStream(fullyQualifiedNamespace);

    protected static string StreamToString(Stream inputStream)
    {
      using (var reader = new StreamReader(inputStream))
        return reader.ReadToEnd();
    }
  }
}
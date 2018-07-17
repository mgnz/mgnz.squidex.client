namespace MGNZ.Squidex.Client.Tests.Stories
{
  using System.Threading.Tasks;

  public class SchemaStories : AuthenticatedSessionStory
  {
    public async Task<dynamic> PostSchema(string app, dynamic schema, string schemaName = null)
    {
      if (string.IsNullOrEmpty(schemaName) || string.IsNullOrWhiteSpace(schemaName) == false)
        schema.name = schemaName;

      return await this.AuthenticatedSchemaClient.CreateSchema(app, schema);
    }

    public async Task<dynamic> GetSchemas(string app)
    {
      return await this.AuthenticatedSchemaClient.GetAllSchemas(app);
    }

    public async Task<dynamic> GetSchema(string app, string schema)
    {
      return await this.AuthenticatedSchemaClient.GetSchema(app, schema);
    }

    public async Task<dynamic> PublishSchema(string app, string schema)
    {
      return await this.AuthenticatedSchemaClient.PublishSchema(app, schema);
    }

    public async Task<dynamic> UnpublishSchema(string app, string schema)
    {
      return await this.AuthenticatedSchemaClient.UnpublishSchema(app, schema);
    }

    public async Task<dynamic> DeleteSchema(string app, string schema)
    {
      return await this.AuthenticatedSchemaClient.DeleteSchema(app, schema);
    }

    public async Task<dynamic> UpdateScripts(string app, string schema, object script)
    {
      return await this.AuthenticatedSchemaClient.UpdateScripts(app, schema, script);
    }
  }
}
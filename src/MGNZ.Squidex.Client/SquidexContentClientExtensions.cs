#if has_typed_client

namespace MGNZ.Squidex.Client
{
  using System;
  using System.Threading.Tasks;
  using MGNZ.Squidex.Client._Model;
  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Transport;

  using Newtonsoft.Json;

  [Obsolete("dont use under development")]
  public static class SquidexContentClientExtensions
  {
    public static async Task<QueryResponse<TModel>> QueryContent_<TModel>(this ISquidexContentClient that, string app, string schema, int top = 20, int skip = 0, string orderBy = null, string search = null, string filter = null)
    {
      return await that.QueryContent_<TModel>(app, schema, new QueryRequest()
      {
        Top = top, Skip = skip,OrderBy = orderBy, Search = search, Filter = filter
      });
    }

    public static async Task<QueryResponse<TModel>> QueryContent_<TModel>(this ISquidexContentClient that, string app, string schema, QueryRequest request)
    {
      dynamic raw = await that.QueryContent(app, schema, request.Top, request.Skip, request.OrderBy, request.Search, request.Filter);
      var deserialized = JsonConvert.DeserializeObject<QueryResponse<TModel>>(raw.ToString());

      return deserialized;
    }

    public static async Task<Entity<TModel>> CreateContent_<TModel>(this ISquidexContentClient that, string app, string schema, TModel content)
    {
      var raw = await that.CreateContent(app, schema, content);
      var deserialized = (Entity<TModel>)JsonConvert.DeserializeObject<Entity<TModel>>(raw.ToString());

      return deserialized;
    }

    public static async Task<ItemContent<TModel>> GetContent_<TModel>(this ISquidexContentClient that, string app, string schema, string id)
    {
      var raw = await that.GetContent(app, schema, id);
      var deserialized = JsonConvert.DeserializeObject<ItemContent<TModel>>(raw);

      return deserialized;
    }

    public static async Task<TModel> UpdateContent_<TModel>(this ISquidexContentClient that, string app, string schema, string id, TModel content)
    {
      var raw = await that.PatchContent(app, schema, id, content);
      var deserialized = JsonConvert.DeserializeObject<TModel>(raw);

      return deserialized;
    }
  }
}

#endif

namespace MGNZ.Squidex.Client
{
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Transport;

  using Newtonsoft.Json;

  public static class SquidexContentClientExtensions
  {
    public static async Task<QueryResponse<TModel>> Query<TModel>(this ISquidexContentClient that, string app, string schema, int top = 20, int skip = 0, string orderBy = null, string search = null, string filter = null)
    {
      return await that.Query<TModel>(app, schema, new QueryRequest()
      {
        Top = top, Skip = skip,OrderBy = orderBy, Search = search, Filter = filter
      });
    }

    public static async Task<QueryResponse<TModel>> Query<TModel>(this ISquidexContentClient that, string app, string schema, QueryRequest request)
    {
      var raw = await that.Query(app, schema, request.Top, request.Skip, request.OrderBy, request.Search, request.Filter);
      var deserialized = JsonConvert.DeserializeObject<QueryResponse<TModel>>(raw);

      return deserialized;
    }

    public static async Task<ItemContent<TModel>> Create<TModel>(this ISquidexContentClient that, string app, string schema, TModel content)
    {
      var raw = await that.Post(app, schema, content);
      var deserialized = JsonConvert.DeserializeObject<ItemContent<TModel>>(raw);

      return deserialized;
    }

    public static async Task<ItemContent<TModel>> Get<TModel>(this ISquidexContentClient that, string app, string schema, string id)
    {
      var raw = await that.Get(app, schema, id);
      var deserialized = JsonConvert.DeserializeObject<ItemContent<TModel>>(raw);

      return deserialized;
    }

    public static async Task<TModel> Update<TModel>(this ISquidexContentClient that, string app, string schema, string id, TModel content)
    {
      var raw = await that.Patch(app, schema, id, content);
      var deserialized = JsonConvert.DeserializeObject<TModel>(raw);

      return deserialized;
    }
  }
}
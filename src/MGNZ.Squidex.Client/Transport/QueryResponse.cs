namespace MGNZ.Squidex.Client.Transport
{
  using MGNZ.Squidex.Client.Model;

  public class QueryResponse<TModel>
  {
    public int Total { get; set; }
    public ItemContent<TModel>[] Items { get; set; }
  }
}
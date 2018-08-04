namespace MGNZ.Squidex.Client.Transport
{
  using MGNZ.Squidex.Client.Model;

  public class SquidexQueryResponse<TModel>
  {
    public int Total { get; set; }
    public SquidexItemContent<TModel>[] Items { get; set; }
  }
}
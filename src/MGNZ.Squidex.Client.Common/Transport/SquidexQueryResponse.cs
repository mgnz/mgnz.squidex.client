namespace MGNZ.Squidex.Client.Common.Transport
{
  using Model;

  public class SquidexQueryResponse<TModel>
  {
    public int Total { get; set; }
    public SquidexItemContent<TModel>[] Items { get; set; }
  }
}
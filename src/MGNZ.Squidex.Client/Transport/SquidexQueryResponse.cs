namespace MGNZ.Squidex.Client.Model
{
  using Model;

  public class SquidexQueryResponse<TModel>
  {
    public int Total { get; set; }
    public SquidexItemContent<TModel>[] Items { get; set; }
  }
}
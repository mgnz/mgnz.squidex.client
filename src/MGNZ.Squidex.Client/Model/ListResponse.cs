namespace MGNZ.Squidex.Client.Model
{
  public class ListResponse<TItemContent>
  {
    public int Total { get; set; }
    public TItemContent[] Items { get; set; }
  }
}
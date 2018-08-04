namespace MGNZ.Squidex.Client.Transport
{
  public class SquidexErrorResponse
  {
    public string Message { get; set; }
    public string[ ] Details { get; set; }
    public int StatusCode { get; set; }
  }
}